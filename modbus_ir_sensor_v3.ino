#include <ModbusMaster.h>               //Library for using ModbusMaster

#define MAX485_DE      32
#define MAX485_RE_NEG  33

#define c1_in 22
#define c2_in 23
#define c3_in 24
#define c4_in 25
#define c5_in 26
#define c6_in 27
#define c7_in 28
#define c8_in 29
#define c9_in 30
#define c10_in 31

struct counter
{
   int output;
   bool flag;
};

typedef struct counter Counter;

Counter c_array[10] = {0};
//int c_in_array[10] = {0};

ModbusMaster node;                    //object node for class ModbusMaster

void preTransmission()            //Function for setting stste of Pins DE & RE of RS-485
{
  digitalWrite(MAX485_RE_NEG, 1);             
  digitalWrite(MAX485_DE, 1);
}

void postTransmission()
{
  digitalWrite(MAX485_RE_NEG, 0);
  digitalWrite(MAX485_DE, 0);
}

void setup() {
  // put your setup code here, to run once:
  pinMode(MAX485_RE_NEG, OUTPUT);
  pinMode(MAX485_DE, OUTPUT);

  pinMode(c1_in, INPUT_PULLUP);
  pinMode(c2_in, INPUT_PULLUP);
  pinMode(c3_in, INPUT_PULLUP);
  pinMode(c4_in, INPUT_PULLUP);
  pinMode(c5_in, INPUT_PULLUP);
  pinMode(c6_in, INPUT_PULLUP);
  pinMode(c7_in, INPUT_PULLUP);
  pinMode(c8_in, INPUT_PULLUP);
  pinMode(c9_in, INPUT_PULLUP);
  pinMode(c10_in, INPUT_PULLUP);
  
  digitalWrite(MAX485_RE_NEG, 0);
  digitalWrite(MAX485_DE, 0);
  
  Serial.begin(115200);
  //Serial.print("Start\r\n");
  node.begin(1, Serial);            //Slave ID as 1
  node.preTransmission(preTransmission);         //Callback for configuring RS-485 Transreceiver correctly
  node.postTransmission(postTransmission);
}

void loop() {
  // put your main code here, to run repeatedly:

  for(int i = 0; i < 10; ++i)
  {
    c_array[i].output = digitalRead(22 + i);
    if (c_array[i].output == 0)
    {
      node.writeSingleRegister((unsigned char)(0x40000 + i),1);               //Writes 1 to 0x40001 holding register
    }
    c_array[i].output = digitalRead(22 + i);                          //Reads state of push button 
    if(c_array[i].output != 0)
    {
      node.writeSingleRegister((unsigned char)(0x40000 + i),0);              //Writes 0 to 0x40001 holding register
    }
    //Serial.print("Sensor ");
    //Serial.print(i+1);
    //Serial.print(": ");
    //Serial.print(c_array[i].output);
    //Serial.print("\n");
    //delay(500);
  }
}
