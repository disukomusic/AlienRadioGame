// Define pin numbers for the potentiometers
const int pot1Pin = A0;
const int pot2Pin = A1;
const int pot3Pin = A2;

// Define pin numbers for the first RGB LED
const int r1 = 5;
const int g1 = 4;
const int b1 = 3;

// Define pin numbers for the second RGB LED
const int r2 = 8;
const int g2 = 7;
const int b2 = 6;

// Define pin numbers for the third RGB LED
const int r3 = 2;
const int g3 = 1;
const int b3 = 0;

// Variable to store potentiometer values
int pot1Value;
int pot2Value;
int pot3Value;

void setup() {
  Serial.begin(115200); // Start serial communication
  // Set RGB LED pins as output
  pinMode(r1, OUTPUT);
  pinMode(g1, OUTPUT);
  pinMode(b1, OUTPUT);
  pinMode(r2, OUTPUT);
  pinMode(g2, OUTPUT);
  pinMode(b2, OUTPUT);
  pinMode(r3, OUTPUT);
  pinMode(g3, OUTPUT);
  pinMode(b3, OUTPUT);
}

void loop() {
  // Read the potentiometer values
  pot1Value = analogRead(pot1Pin);
  pot2Value = analogRead(pot2Pin);
  pot3Value = analogRead(pot3Pin);
  
  // Send pot values to Unity
  Serial.print(pot1Value);
  Serial.print(",");
  Serial.print(pot2Value);
  Serial.print(",");
  Serial.println(pot3Value);
  
  // Check if there's data available from Unity
  if (Serial.available() > 0) {
    String data = Serial.readStringUntil('\n');
    String values[3];
    int index = 0;
    
    // Split the received data into values
    for (int i = 0; i < data.length(); i++) {
      if (data.charAt(i) == ',') {
        index++;
      } else {
        values[index] += data.charAt(i);
      }
    }
    
    // Check if we received 3 values
    if (index == 2) {
      float volume1 = values[0].toFloat();
      float volume2 = values[1].toFloat();
      float volume3 = values[2].toFloat();
      
      // Control LEDs based on volume values
      if (volume1 > 0.5) {
        setColor(r1, g1, b1, 255, 0, 0); // Turn on red LED for pot 1
      } else {
        setColor(r1, g1, b1, 0, 0, 0); // Turn off LED
      }
      
      if (volume2 > 0.5) {
        setColor(r2, g2, b2, 0, 255, 0); // Turn on green LED for pot 2
      } else {
        setColor(r2, g2, b2, 0, 0, 0); // Turn off LED
      }
      
      if (volume3 > 0.5) {
        setColor(r3, g3, b3, 225, 0, 0); // Turn on blue LED for pot 3
      } else {
        setColor(r3, g3, b3, 0, 0, 0); // Turn off LED
      }
    }
  }
  
  delay(25); // Delay to avoid overwhelming the serial buffer
}

void setColor(int redPin, int greenPin, int bluePin, int redValue, int greenValue, int blueValue) {
  analogWrite(redPin, redValue);
  analogWrite(greenPin, greenValue);
  analogWrite(bluePin, blueValue);
}
