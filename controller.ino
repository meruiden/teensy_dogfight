#include <Servo.h>

class Reader{
  private:
    static const int numReadings = 20;
    int readings[numReadings];
    int readIndex;
    double total;
    double average;

  public:
    Reader(){
      readIndex = 0;
      total = 0;
      average = 0;
      for (int i = 0; i < numReadings; i++) {
        readings[i] = 0;
      }
    }

    double calculate(double data){
      if(data == 0){
        return data;
      }
      total = total - readings[readIndex];
      readings[readIndex] = data;
      total = total + readings[readIndex];
      readIndex = readIndex + 1;
      if (readIndex >= numReadings) {
        readIndex = 0;
      }
      average = (double)(total / numReadings);
      return average;
    }
  
};

int c1Xpin = A0;
int c1Ypin = A1;
int c1PressPin = 16;
int c1FirePin = 11;
double c1X = 0;
double c1Y = 0;
bool c1FirePressed = false;
Reader c1RX = Reader();
Reader c1RY = Reader();

float deadZone = 0.05f;

int c2Xpin = A3;
int c2Ypin = A4;
int c2PressPin = 19;
int c2FirePin = 12;
double c2X = 0;
double c2Y = 0;
bool c2FirePressed = false;
Reader c2RX = Reader();
Reader c2RY = Reader();

void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);

  pinMode(c1Xpin, INPUT);
  pinMode(c1Ypin, INPUT);
  pinMode(c1PressPin, INPUT_PULLUP);
  pinMode(c1FirePin, INPUT_PULLUP);
  
  pinMode(c2Xpin, INPUT);
  pinMode(c2Ypin, INPUT);
  pinMode(c2PressPin, INPUT_PULLUP);
  pinMode(c2FirePin, INPUT_PULLUP);
  
  pinMode(13, OUTPUT);
  digitalWrite(13, HIGH);
}

void loop() {
  handleJoySticks();
}

void handleJoySticks(){
    
  c1X = c1RX.calculate(analogRead(c1Xpin));
  c1X -= 526.99;
  
  if(c1X < 0){
  
    c1X *= 1.0/527.0;
  }

  if(c1X > 0){
    c1X *= 1.0/489.0;
  }
  if(c1X > 1){
    c1X = 1;
  }
  if(c1X < -1){
    c1X = -1;
  }

  c1Y = c1RY.calculate(analogRead(c1Ypin));
 
  c1Y -= 531.99;
  if(c1Y < 0){
    c1Y *= 1.0f/532.0;
  }

  if(c1Y > 0){
    c1Y *= 1.0f/481.0;
  }
  if(c1Y > 1){
    c1Y = 1;
  }
  if(c1Y < -1){
    c1Y = -1;
  }

  
  if(c1X <= deadZone && c1X >= -deadZone){
    c1X = 0;
  }

   if(c1Y <= deadZone && c1Y >= -deadZone){
    c1Y = 0;
  }
  String toPrint;
  toPrint += c1X;
  toPrint += ",";
  toPrint += c1Y;
  if(digitalRead(c1PressPin) == LOW){
    toPrint += ",1,";
  }else{
    toPrint += ",0,";
  }

  c2X = c2RX.calculate(analogRead(c2Xpin));
  c2X -= 526.99;
  
  if(c2X < 0){
  
    c2X *= 1.0/527.0;
  }

  if(c2X > 0){
    c2X *= 1.0/489.0;
  }
  if(c2X > 1){
    c2X = 1;
  }
  if(c2X < -1){
    c2X = -1;
  }

  c2Y = c2RY.calculate(analogRead(c2Ypin));
 
  c2Y -= 531.99;
  if(c2Y < 0){
    c2Y *= 1.0f/532.0;
  }

  if(c2Y > 0){
    c2Y *= 1.0f/481.0;
  }
  if(c2Y > 1){
    c2Y = 1;
  }
  if(c2Y < -1){
    c2Y = -1;
  }

  
  if(c2X <= deadZone && c2X >= -deadZone){
    c2X = 0;
  }

   if(c2Y <= deadZone && c2Y >= -deadZone){
    c2Y = 0;
  }
  
  toPrint += c2X;
  toPrint += ",";
  toPrint += c2Y;
  if(digitalRead(c2PressPin) == LOW){
    toPrint += ",1";
  }else{
    toPrint += ",0";
  }

  if(digitalRead(c1FirePin) == LOW){
    toPrint += ",1";
  }else{
    toPrint += ",0";
  }

  if(digitalRead(c2FirePin) == LOW){
    toPrint += ",1";
  }else{
    toPrint += ",0";
  }

  Serial.println(toPrint);
}

