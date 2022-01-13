#include <PubSubClient.h>
#include <ESP8266WiFi.h>
#include <ESP8266Ping.h>

WiFiClient espClient;
PubSubClient client(espClient);

const char* RABBITMQ_BROKER = "192.168.0.12";
int        RABBITMQ_PORT     = 1883;
const char* RABBITMQ_TOPIC  = "room_measurements";
const char* RABBITMQ_USER = "guest";
const char* RABBITMQ_PASSWORD = "guest";
int         RABBITMQ_SENSOR_ID = 968376;

const long interval = 5000;
unsigned long previousMillis = 0;
unsigned long lastSend;

void reconnect() {
  while (!client.connected()) {
  
  Serial.print("reconnect...");
  if (client.connect("com.gonzalo789.esp32", RABBITMQ_USER, RABBITMQ_PASSWORD)) {
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

void publish_measurements(float temperature, float humidity, int illuminance){
  if ( millis() - lastSend > interval ){
    
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
  char sensorIdChar[64];
  ret = snprintf(sensorIdChar, sizeof sensorIdChar, "%f", RABBITMQ_SENSOR_ID);
  if (ret < 0) {
      Serial.println("Error while convertin int to char array");
      return;
  }
  if (ret >= sizeof sensorIdChar) {
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
  strcat(measurement, sensorIdChar);

  Serial.print("measurement: ");
  Serial.println(measurement);

  client.publish(RABBITMQ_TOPIC, measurement);

  lastSend = millis();

  free(measurement);
  }
}

void callback(char* topic, byte* payload, unsigned int length) {
Serial.print("Message arrived [");
}
