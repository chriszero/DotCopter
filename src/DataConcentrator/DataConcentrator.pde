#include "pins_arduino.h"
#include <Wire.h>

#define CPU_PRESCALE(n) (CLKPR = 0x80, CLKPR = (n))

#define ADDRESS 0x42

#define CHANNEL_COUNT 5

#define RISING_EDGE 1
#define FALLING_EDGE 0
#define MINONWIDTH 950
#define MAXONWIDTH 2075
#define MINOFFWIDTH 12000
#define MAXOFFWIDTH 24000

typedef struct 
{
	unsigned char edge;
	unsigned long riseTime;    
	unsigned long fallTime; 
	unsigned long lastGoodWidth;
} pinTimingData; 

//GLOBALS
volatile pinTimingData pindata[CHANNEL_COUNT];
volatile unsigned int channelBuffer[CHANNEL_COUNT];
HardwareSerial Serial1 = HardwareSerial();

void setup()
{
  int i;
  CPU_PRESCALE(0);

  PCICR |= _BV(PCIE0); //Enable PCINT Interrupts
  for(i=0;i<CHANNEL_COUNT;i++)
    PCMSK0 |= _BV(i); //Enable the required PCINT pins
      
  Wire.begin(ADDRESS);
  Wire.onRequest(requestEvent);
  
  Serial.begin(115200);
  Serial1.begin(115200);
}

void loop()
{
  byte data;
  
  if(Serial.available()>0)
    Serial1.write(Serial.read());
  if(Serial1.available()>0)
    Serial.write(Serial1.read());
    
}

void requestEvent()
{
  int i;
  for(i=0;i<CHANNEL_COUNT;i++)
    channelBuffer[i] = pindata[i].lastGoodWidth;  
  Wire.send((byte *)channelBuffer, sizeof(unsigned int) * CHANNEL_COUNT);
}

static volatile unsigned char lastPinData;
SIGNAL(PCINT0_vect)
{
  int i;
  unsigned char currentPinData;
  unsigned char changedPinMask;
  unsigned long currentTime;
  unsigned long highTime;

  currentPinData = PINB; //Retrieve the current pin states
  changedPinMask = currentPinData ^ lastPinData; //Using an XOR we can generate a mask of only the changed pins
  lastPinData = currentPinData; //Store the current pin state for next time

  currentTime = micros();

  for(i=0;i<CHANNEL_COUNT;i++) 
  {
    if(changedPinMask & (1<<i)) //Make sure the pin has changed
    {
      if(currentPinData & (1<<i)) //Rising Edge
      {
        highTime = pindata[i].fallTime - currentTime;
        pindata[i].riseTime = currentTime;
        if ((highTime >= MINOFFWIDTH) && (highTime <= MAXOFFWIDTH))
          pindata[i].edge = RISING_EDGE;
        else
          pindata[i].edge == FALLING_EDGE; // invalid rising edge detected
      }
      else //Falling Edge
      {
        pindata[i].fallTime = currentTime;
        pindata[i].lastGoodWidth = currentTime - pindata[i].riseTime;
      }
    }
  }
}

