#include "DHT.h"

float readTemperature(DHT *dht){
  float t = dht->readTemperature();
 
  if (isnan(t))
  {
    Serial.println("Error while reading current temperature value");
  }
  
  return t;
}

float readHumidity(DHT *dht){
  float h = dht->readHumidity();
 
  if (isnan(h))
  {
    Serial.println("Error while reading current humidity value");
  }
  
  return h;
}
