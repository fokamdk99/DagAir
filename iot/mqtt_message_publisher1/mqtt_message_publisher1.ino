#include <SoftwareSerial.h>
#include <WiFiEsp.h>
#include <PubSubClient.h>

SoftwareSerial esp8266(4,5); // make RX Arduino line is pin 2, make TX Arduino line is pin 3.
                             // This means that you need to connect the TX line from the esp to the Arduino's pin 2
                             // and the RX line from the esp to the Arduino's pin 3

int status = WL_IDLE_STATUS;

WiFiEspClient espClient;
PubSubClient client(espClient);

const char broker[] = "192.168.0.12";
int        port     = 1883;
const char topic[]  = "room_measurements";

const long interval = 4000;
unsigned long previousMillis = 0;
unsigned long lastSend;

void setup()
{
  Serial.begin(9600);
  //esp8266.begin(9600); // your esp's baud rate might be different

  InitWiFi();

  Serial.println(broker);

  client.setServer(broker, port);
}
 
void loop()
{
  if ( !client.connected() ) {
    reconnect();
  }

  if ( millis() - lastSend > interval ){
    String Rvalue = "temperature: 17C";
    char attributes[100];
    Rvalue.toCharArray(attributes,16);
    
    Serial.print("Sending message to topic: ");
    Serial.println(topic);
    Serial.println(Rvalue);

    client.publish(topic, attributes);

    lastSend = millis();
  }
  
  if(esp8266.available()) // check if the esp is sending a message 
  {
    while(esp8266.available())
    {
      // The esp has data so display its output to the serial window 
      char c = esp8266.read(); // read the next character.
      Serial.write(c);
    }  
  }
   
  if(Serial.available())
  {
    // the following 16 is required because otherwise the arduino will read the first letter of the command but not the rest
    // In other words without the delay if you use AT+RST, for example, the Arduino will read the letter A send it, then read the rest and send it
    // but we want to send everything at the same time.
    delay(1000); 

    String command="";
    
    while(Serial.available()) // read the command character by character
    {
        // read one character
      command+=(char)Serial.read();
    }
    Serial.println(command); // send the read character to the esp8266
    esp8266.println(command); // send the read character to the esp8266
  }

  client.loop();
}

void sendData(String command, const int timeout, char readReplay[])
{
  boolean found = false;
  
  esp8266.print(command); // send the read character to the esp8266
  
  long int time = millis();
  
  while( (time+timeout) > millis())
  {
    if(esp8266.find(readReplay)) {
      found = true;
    }
  }

   if (found) {
    Serial.println("EVERYTHING OK");  
   }
   else {
    Serial.println("SOME ERROR OCCURRED");  
   }
}

void InitWiFi()
{
  // initialize serial for ESP module
  esp8266.begin(9600); 
  // initialize ESP module
  WiFi.init(&esp8266);
  // check for the presence of the shield
  if (WiFi.status() == WL_NO_SHIELD) {
    Serial.println("WiFi shield not present");
    // don't continue
    while (true);
  }

  Serial.println("Connecting to AP ...");
  // attempt to connect to WiFi network
  while ( status != WL_CONNECTED) {
    Serial.print("Attempting to connect to WPA SSID: ");
    Serial.println("Tech_D0054234");
    // Connect to WPA/WPA2 network
    status = WiFi.begin("Tech_D0054234", "XWFKMBRX");
    delay(5000);
  }
  Serial.println("Connected to AP");
}

void reconnect() {
  // Loop until we're reconnected
  while (!client.connected()) {
    Serial.print("Connecting to Thingsboard node ...");
    // Attempt to connect (clientId, username, password)
    if ( client.connect("Arduino Uno Device", "guest", "guest") ) {
      Serial.println( "[DONE]" );
    } else {
      Serial.print( "[FAILED] [ rc = " );
      Serial.print( client.state() );
      Serial.println( " : retrying in 8 seconds]" );
      // Wait 5 seconds before retrying
      delay( 8000 );
    }
  }
}
