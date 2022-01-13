#include "sensor_data.h"
#include "rabbitmq_handler.h"

#define DHTPIN 4
#define DHTTYPE DHT11

DHT dht(DHTPIN, DHTTYPE);
#define WIFI_SSID "Tech_D0054234"
#define WIFI_PASS "XWFKMBRX"

int ILLUMINANCEPIN = A0;
 
void setup() {
  Serial.begin(115200);
  dht.begin();
  Serial.println();
 
  WiFi.mode(WIFI_STA);
  WiFi.begin(WIFI_SSID, WIFI_PASS);
 
  Serial.print("Connecting to ");
  Serial.print(WIFI_SSID);
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(100);
    Serial.print(".");
  }
 
  // Connected to WiFi
  Serial.println();
  Serial.print("Connected! IP address: ");
  Serial.println(WiFi.localIP());
  
  client.setServer(RABBITMQ_BROKER, RABBITMQ_PORT);
  client.setCallback(callback);
 
}
 
void loop() {
  if ( !client.connected() ) {
    reconnect();
  }
  
  float temperature = readTemperature(&dht);
  float humidity = readHumidity(&dht);
  int illuminance = analogRead(ILLUMINANCEPIN);
  publish_measurements(temperature, humidity, illuminance);
  
  delay(1000);

  client.loop();
}
