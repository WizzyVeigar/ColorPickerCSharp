#include <Braccio.h>;
#include <Servo.h>

Servo base;
Servo shoulder;
Servo elbow;
Servo wrist_rot;
Servo wrist_ver;
Servo gripper;

int switchState;
int switchState2;

void setup() {
  Braccio.begin();
  Braccio.ServoMovement(20,           0, 97, 98, 90, 90, 10 );
  Serial.begin(9600);  // set serial port at desired value
}

void loop() {
  switchState = digitalRead(2);
  switchState2 = digitalRead(4);

  if (switchState == HIGH) //FIRST BUTTON!
  {
    Serial.print("h");
    delay(200);

  }
  if (switchState2 == HIGH) //SECOND BUTTON!
  {
    Serial.print("t");
    delay(200);
  }

  while (Serial.available()) {
    char inFromPc = Serial.read();

    switch (inFromPc) {
      case 's': //start
        Braccio.ServoMovement(20,                0, 91, 16, 180, 90, 30 );
        Braccio.ServoMovement(20,                0, 91, 20, 170, 90, 30 );//Move down to bowl
        Braccio.ServoMovement(20,                0, 91, 14, 180, 90, 40 );//Move back and forth
        Braccio.ServoMovement(20,                0, 91, 14, 170, 90, 40 ); 
        Braccio.ServoMovement(20,                0, 91, 14, 155, 90, 40 ); 
        Braccio.ServoMovement(20,                0, 91, 14, 179, 90, 40 ); 
        Braccio.ServoMovement(20,                0, 91, 14, 179, 90, 69 ); //Grab M%M
        Braccio.ServoMovement(20,                0, 120, 15, 160, 90, 69 ); //Move to camera
        Braccio.ServoMovement(20,                0, 71, 45, 154, 90, 69 );
        break;
      case 'w':
        Braccio.ServoMovement(20,                0, 91, 16, 180, 90, 30 );
        Braccio.ServoMovement(20,                0, 91, 20, 170, 90, 30 );//Move to down to bowl
        Braccio.ServoMovement(20,                0, 91, 14, 180, 90, 40 );//Move back and forth
        Braccio.ServoMovement(20,                0, 91, 14, 170, 90, 40 );
        Braccio.ServoMovement(20,                0, 87, 12, 155, 90, 40 );
        Braccio.ServoMovement(20,                0, 87, 12, 158, 90, 40 );
        Braccio.ServoMovement(20,                0, 87, 12, 158, 90, 69 ); //Grab M%M
        Braccio.ServoMovement(20,                0, 120, 15, 160, 90, 69 ); //Move to camera
        Braccio.ServoMovement(20,                0, 71, 45, 154, 90, 69 );
        break;
      case 'r': //reset
        Braccio.ServoMovement(20,                0, 97, 98, 90, 90, 10 ); //Arm stands straight up
        break;
      case 'a': //Red
        Braccio.ServoMovement(20,                55, 78, 55, 160, 90, 69 ); //Moves to red pile/bowl
        Braccio.ServoMovement(20,                55, 78, 55, 160, 90, 10 ); //Drops M&M into pile/bowl
        Braccio.ServoMovement(20,                0, 120, 15, 180, 90, 73 );
        break;
      case 'b': //Orange
        Braccio.ServoMovement(20,                80, 78, 55, 160, 90, 69 ); //Moves to orange pile/bowl
        Braccio.ServoMovement(20,                80, 78, 55, 160, 90, 10 ); //Drops M&M into pile/bowl
        Braccio.ServoMovement(20,                0, 120, 15, 180, 90, 73 );
        break;
      case 'c': //Yellow
        Braccio.ServoMovement(20,                105, 78, 55, 160, 90, 69 ); //Moves to yellow pile/bowl
        Braccio.ServoMovement(20,                105, 78, 55, 160, 90, 10 ); //Drops M&M into pile/bowl
        Braccio.ServoMovement(20,                0, 120, 15, 180, 90, 73 );
        break;
      case 'd': //Green
        Braccio.ServoMovement(20,                130, 78, 55, 160, 90, 69 );//Moves to green pile/bowl
        Braccio.ServoMovement(20,                130, 78, 55, 160, 90, 10 );//Drops M&M into pile/bowl
        Braccio.ServoMovement(20,                0, 120, 15, 180, 90, 73 );
        break;
      case 'e': //Blue
        Braccio.ServoMovement(20,                155, 78, 55, 160, 90, 69 );//Moves to blue pile/bowl
        Braccio.ServoMovement(20,                155, 78, 55, 160, 90, 10 );//Drops M&M into pile/bowl
        Braccio.ServoMovement(20,                0, 120, 15, 180, 90, 73 );
        break;
      case 'f': //Brown
        Braccio.ServoMovement(20,                180, 78, 55, 160, 90, 69 );//Moves to brown pile/bowl
        Braccio.ServoMovement(20,                180, 78, 55, 160, 90, 10 );//Drops M&M into pile/bowl
        Braccio.ServoMovement(20,                0, 120, 15, 180, 90, 73 );
        break;
      default:
        break;
    }
  }
}
