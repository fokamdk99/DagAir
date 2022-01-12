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

const long interval = 4000;
unsigned long previousMillis = 0;
unsigned long lastSend;

void reconnect() {
// Loop until we're reconnected
while (!client.connected()) {
  
Serial.print("reconnect...");
// Attempt to connect
if (client.connect("com.gonzalo789.esp32", RABBITMQ_USER, RABBITMQ_PASSWORD)) {
Serial.println("connected");
// Once connected, publish an announcement...
client.publish(RABBITMQ_TOPIC, "halo world");
} else {
  Serial.print("failed, rc=");
  Serial.print(client.state());
  Serial.println(" try again in 5 seconds");
  // Wait 5 seconds before retrying
  delay(5000);
    }
  }
}

void publish_measurements(float temperature, float humidity, int illuminance){
    if ( millis() - lastSend > interval ){

        String Rvalue = "temperature: 17C";
        
        Serial.print("Sending message to topic: ");
        //Serial.println(topic);
        Serial.println(Rvalue);

        //client.publish(topic, attributes);

        lastSend = millis();
    }
}

void callback(char* topic, byte* payload, unsigned int length) {
Serial.print("Message arrived [");
}

void myPrintf(float fVal)
{
    char result[100];
    int dVal, dec, i;

    fVal += 0.005;   // added after a comment from Matt McNabb, see below.

    dVal = fVal;
    dec = (int)(fVal * 100) % 100;

    memset(result, 0, 100);
    result[0] = (dec % 10) + '0';
    result[1] = (dec / 10) + '0';
    result[2] = '.';

    i = 3;
    while (dVal > 0)
    {
        result[i] = (dVal % 10) + '0';
        dVal /= 10;
        i++;
    }

    for (i=strlen(result)-1; i>=0; i--)
        putc(result[i], stdout);
}
