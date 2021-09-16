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
  Braccio.ServoMovement(20,           0, 49, 98, 90, 90, 73 ); //97
  Serial.begin(9600);  // set serial port at desired value
}

void loop() {
  switchState = digitalRead(2);
  switchState2 = digitalRead(4);

  if (switchState == HIGH) //PURPLE BUTTON!
  {
    Serial.print("h");
    delay(200);

  }
  if (switchState2 == HIGH) //GREEN BUTTON!
  {
    Serial.print("t");
    delay(200);
  }

  while (Serial.available()) {
    char inFromPc = Serial.read();

    switch (inFromPc) {
      case 's': //start
        Braccio.ServoMovement(20,                0, 30, 50, 180, 90, 30 );
        Braccio.ServoMovement(10,                0, 44, 20, 175, 90, 30 );//Move down to bowl
        Braccio.ServoMovement(10,                0, 44, 20, 180, 90, 35 );//Move back and forth
        Braccio.ServoMovement(10,                0, 44, 18, 170, 90, 35 );
        Braccio.ServoMovement(10,                0, 42, 15, 155, 90, 35 );
        Braccio.ServoMovement(10,                0, 44, 19, 179, 90, 35 );
        Braccio.ServoMovement(10,                0, 44, 20, 180, 90, 35 );
        Braccio.ServoMovement(30,                0, 44, 15, 165, 90, 35 );
        Braccio.ServoMovement(30,                0, 44, 15, 165, 90, 66 ); //Grab M%M
        Braccio.ServoMovement(20,                0, 55, 50, 180, 90, 66 );
        Braccio.ServoMovement(20,                0, 55, 50, 180, 90, 66 );
        Braccio.ServoMovement(30,                0, 75, 23, 160, 90, 66 ); //Move to camera
        Braccio.ServoMovement(30,                0, 25, 45, 154, 90, 66 );
        break;
      case 'w':
        Braccio.ServoMovement(20,                0, 30, 50, 180, 90, 30 );
        Braccio.ServoMovement(20,                0, 45, 15, 155, 90, 45 ); //Start left to right Scoop
        Braccio.ServoMovement(20,                0, 45, 17, 165, 90, 45 );
        Braccio.ServoMovement(20,                0, 50, 10, 170, 90, 45 ); //End Scoop
        Braccio.ServoMovement(20,                0, 55, 50, 180, 90, 45 ); //Going back up from bowl

        Braccio.ServoMovement(20,                0, 75, 10, 180, 90, 45 ); //Start right to left Scoop
        Braccio.ServoMovement(20,                0, 60, 10, 180, 90, 45 );
        Braccio.ServoMovement(20,                0, 51, 10, 180, 90, 45 ); //End Scoop
        Braccio.ServoMovement(20,                0, 35, 50, 180, 90, 30 ); //Going back up from bowl
        Braccio.ServoMovement(20,                0, 35, 50, 180, 90, 35 );
        Braccio.ServoMovement(20,                0, 50, 10, 180, 90, 35 );
        Braccio.ServoMovement(20,                0, 50, 10, 180, 90, 66 ); //Grab M%M
        Braccio.ServoMovement(20,                0, 55, 50, 180, 90, 66 );
        Braccio.ServoMovement(20,                0, 55, 50, 180, 90, 66 );
        Braccio.ServoMovement(30,                0, 75, 23, 160, 90, 66 ); //Move to camera
        Braccio.ServoMovement(30,                0, 25, 45, 154, 90, 66 );
        break;
      case 'r': //reset
        Braccio.ServoMovement(20,                0, 52, 98, 90, 90, 30 ); //Arm stands straight up
        break;
      case 'a': //Red
        Braccio.ServoMovement(20,                55, 33, 55, 160, 90, 69 ); //Moves to red pile/bowl
        Braccio.ServoMovement(20,                55, 33, 55, 160, 90, 30 ); //Drops M&M into pile/bowl
        Braccio.ServoMovement(20,                0, 75, 15, 180, 90, 73 );
        break;
      case 'b': //Orange
        Braccio.ServoMovement(20,                80, 33, 55, 160, 90, 69 ); //Moves to orange pile/bowl
        Braccio.ServoMovement(20,                80, 33, 55, 160, 90, 30 ); //Drops M&M into pile/bowl
        Braccio.ServoMovement(20,                0, 75, 15, 180, 90, 73 );
        break;
      case 'c': //Yellow
        Braccio.ServoMovement(20,                105, 33, 55, 160, 90, 69 ); //Moves to yellow pile/bowl
        Braccio.ServoMovement(20,                105, 33, 55, 160, 90, 30 ); //Drops M&M into pile/bowl
        Braccio.ServoMovement(20,                0, 75, 15, 180, 90, 73 );
        break;
      case 'd': //Green
        Braccio.ServoMovement(20,                130, 33, 55, 160, 90, 69 );//Moves to green pile/bowl
        Braccio.ServoMovement(20,                130, 33, 55, 160, 90, 30 );//Drops M&M into pile/bowl
        Braccio.ServoMovement(20,                0, 75, 15, 180, 90, 73 );
        break;
      case 'e': //Blue
        Braccio.ServoMovement(20,                155, 33, 55, 160, 90, 69 );//Moves to blue pile/bowl
        Braccio.ServoMovement(20,                155, 33, 55, 160, 90, 30 );//Drops M&M into pile/bowl
        Braccio.ServoMovement(20,                0, 75, 15, 180, 90, 73 );
        break;
      case 'f': //Brown
        Braccio.ServoMovement(20,                180, 33, 55, 160, 90, 69 );//Moves to brown pile/bowl
        Braccio.ServoMovement(20,                180, 33, 55, 160, 90, 30 );//Drops M&M into pile/bowl
        Braccio.ServoMovement(20,                0, 75, 15, 180, 90, 73 );
        break;
      default:
        break;
    }
  }
}
