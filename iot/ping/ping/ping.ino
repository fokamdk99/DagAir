#include <ESP8266WiFi.h>
#include <ESP8266Ping.h>

// Update these with values suitable for your network.

const char* ssid = "Tech_D0054234";
const char* password = "XWFKMBRX";

const IPAddress remote_ip1(8, 8, 8, 8);
const IPAddress remote_ip2(192, 168, 0, 12);

int pingIP1count = 0;
int pingIP1fails = 0;
int pingIP1failmax = 3;
int pingIP2count = 0;
int pingIP2fails = 0;
int pingIP2failmax = 3;
int retryTime = 300000;
int retryTimeMins = retryTime / 60000;  //convert time to minutes

WiFiClient espClient;

void setup() {
  Serial.begin(115200);
  setup_wifi();

}

void setup_wifi() {

  delay(10);
  // We start by connecting to a WiFi network
  Serial.println();
  Serial.print("Connecting to ");
  Serial.println(ssid);

  WiFi.begin(ssid, password);

  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }

  randomSeed(micros());

  Serial.println("");
  Serial.println("WiFi connected");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());
}

void loop() { 
  bool pingCheck1 = Ping.ping(remote_ip1);
  bool pingCheck2 = Ping.ping(remote_ip2);

  if(pingCheck1){
    Serial.print("Google Success! ");
  }
  else {
    Serial.print("Google Fail! ");
  }

  if(pingCheck2){
    Serial.print("Localhost Success! ");
  }
  else {
    Serial.print("Localhost Fail! ");
  }

  delay(5500);
}
