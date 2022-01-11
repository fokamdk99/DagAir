#include <ESP8266WiFi.h>
#include "sensor_data.h"
 
#define DHTPIN 4          // numer pinu sygnałowego
#define DHTTYPE DHT11     // typ czujnika (DHT11). Jesli posiadamy DHT22 wybieramy DHT22

 
DHT dht(DHTPIN, DHTTYPE); // definicja czujnika
// Set WiFi credentials
#define WIFI_SSID "Tech_D0054234"
#define WIFI_PASS "XWFKMBRX"

int ILLUMINANCEPIN = A0;
 
void setup() {
  // Setup serial port
  Serial.begin(115200);
  dht.begin();
  Serial.println();
 
  // Begin WiFi
  WiFi.begin(WIFI_SSID, WIFI_PASS);
 
  // Connecting to WiFi...
  Serial.print("Connecting to ");
  Serial.print(WIFI_SSID);
  // Loop continuously while WiFi is not connected
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(100);
    Serial.print(".");
  }
 
  // Connected to WiFi
  Serial.println();
  Serial.print("Connected! IP address: ");
  Serial.println(WiFi.localIP());
 
}
 
void loop() {
  float temperature = readTemperature(&dht);
  float humidity = readHumidity(&dht);
  int illuminance = analogRead(ILLUMINANCEPIN);
  Serial.print("Illuminance: ");
  Serial.print(illuminance);
  delay(1000);
}
