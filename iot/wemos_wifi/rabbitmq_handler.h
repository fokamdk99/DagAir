#include <PubSubClient.h>
#include <ESP8266WiFi.h>
#include <ESP8266Ping.h>
#include <stdio.h>
#include <time.h>
#include "sensor_data.h"

WiFiClient espClient;
PubSubClient client(espClient);

const char* RABBITMQ_BROKER = "192.168.0.12";
int        RABBITMQ_PORT     = 1883;
const char* RABBITMQ_TOPIC  = "room_measurements";
const char* RABBITMQ_SUBSCRIPTION  = "request_measurement";
const char* RABBITMQ_USER = "guest";
const char* RABBITMQ_PASSWORD = "guest";
const char* RABBITMQ_SENSOR_ID = "968376";

const long interval = 5000;
unsigned long previousMillis = 0;
unsigned long lastSend;

void reconnect() {
  while (!client.connected()) {
  
  Serial.print("reconnect...");
  if (client.connect("com.gonzalo789.esp32", RABBITMQ_USER, RABBITMQ_PASSWORD)) {
    boolean r= client.subscribe(RABBITMQ_SUBSCRIPTION);
    Serial.println("connected");
  } 
  else {
    Serial.print("failed, rc=");
    Serial.print(client.state());
    Serial.println(" try again in 5 seconds");
    delay(5000);
    }
  }
}

void send_measurements(float temperature, float humidity, int illuminance){
  char temperatureChar[64];
  int ret = snprintf(temperatureChar, sizeof temperatureChar, "%f", temperature);
  if (ret < 0) {
      Serial.println("Error while convertin float to char array");
      return;
  }
  if (ret >= sizeof temperatureChar) {
       Serial.println("Value too large");
       return;
  }
  char illuminanceChar[64];
  ret = snprintf(illuminanceChar, sizeof illuminanceChar, "%d", illuminance);
  if (ret < 0) {
      Serial.println("Error while convertin int to char array");
      return;
  }
  if (ret >= sizeof illuminanceChar) {
       Serial.println("Value too large");
       return;
  }
  char humidityChar[64];
  ret = snprintf(humidityChar, sizeof humidityChar, "%f", humidity);
  if (ret < 0) {
      Serial.println("Error while convertin float to char array");
      return;
  }
  if (ret >= sizeof humidityChar) {
       Serial.println("Value too large");
       return;
  }

  char* measurement = (char*)malloc(256+1+3); //4*64 + 3 semicolons + EOF
  strcpy(measurement, temperatureChar);
  strcat(measurement, ";");
  strcat(measurement, illuminanceChar);
  strcat(measurement, ";");
  strcat(measurement, humidityChar);
  strcat(measurement, ";");
  strcat(measurement, RABBITMQ_SENSOR_ID);

  time_t t = time(NULL);
  struct tm tm = *localtime(&t);
  printf("now: %d-%02d-%02d %02d:%02d:%02d\n", tm.tm_year + 1900, tm.tm_mon + 1, tm.tm_mday, tm.tm_hour, tm.tm_min, tm.tm_sec);
  
  Serial.print("measurement: ");
  Serial.println(measurement);

  client.publish(RABBITMQ_TOPIC, measurement);

  free(measurement);
}

void publish_measurements(float temperature, float humidity, int illuminance, bool forceSend){
  if (forceSend == true){
    send_measurements(temperature, humidity, illuminance);
    return;
  }
  
  if ( millis() - lastSend > interval ){
    send_measurements(temperature, humidity, illuminance);
    lastSend = millis();
  }
}

void callback(char* topic, byte* payload, unsigned int length) {
  Serial.print("Message arrived on topic: ");
  Serial.println(topic);
  Serial.print("length message received in callback= ");
  Serial.println(length);
  
  char* sensorId = (char*)payload;

  String messageTemp;
  
  for (int i = 0; i < length; i++) {
    Serial.print((char)payload[i]);
    messageTemp += (char)payload[i];
  }
  Serial.println();

  if(strcmp(topic, RABBITMQ_SUBSCRIPTION) == 0 && strcmp(sensorId, RABBITMQ_SENSOR_ID)){
    Serial.println("Measurements requested, sending...");
    float temperature = readTemperature(&dht);
    float humidity = readHumidity(&dht);
    int illuminance = analogRead(ILLUMINANCEPIN);
    publish_measurements(temperature, humidity, illuminance, true);
  }
}
