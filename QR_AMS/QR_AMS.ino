#include "ESPino32CAM.h"
#include "ESPino32CAM_QRCode.h"


// white gnd, black 5v, red rx, brown tx
ESPino32CAM cam;
ESPino32QRCode qr;

#define LED 33
#define BUTTON_QR 4

#define CAMERA_MODEL_AI_THINKER

void setup()
{
  Serial.begin(115200);
  pinMode(LED, OUTPUT);
  pinMode(BUTTON_QR, OUTPUT); 
  Serial.println("ESP32 CAM SERIAL TEST");
  if (cam.init() != ESP_OK)
  {
    Serial.println(F("Fail"));
    while (1);
  }
  
  qr.init(&cam);
  sensor_t *s = cam.sensor();
  s->set_framesize(s, FRAMESIZE_CIF);
  s->set_whitebal(s, true); 
  digitalWrite(LED, HIGH);

//  analogWrite(6,con);  
 // lcd.begin(16,2);
 // Serial.begin(9600);
 // lcd.clear();
}

void loop()
{
  unsigned long pv_time  = millis();
  camera_fb_t *fb = cam.capture();
  if (!fb)
  {
    Serial.println("Camera capture failed");
    return;
  }
  dl_matrix3du_t *rgb888, *rgb565;
  if (cam.jpg2rgb(fb, &rgb888))
  {
    rgb565 = cam.rgb565(rgb888);   
  }
  cam.clearMemory(rgb888);
  cam.clearMemory(rgb565); 
  dl_matrix3du_t *image_rgb;
  
  if (cam.jpg2rgb(fb, &image_rgb))
  {
    cam.clearMemory(fb);
    if (!digitalRead(BUTTON_QR))
    {
      qrResoult res = qr.recognition(image_rgb);
           
      if (res.status)
      { 
        digitalWrite(LED, LOW);              
        String text = "QR Read:" + res.payload;
        Serial.print("#");
        Serial.print(res.payload); 
        Serial.print(" ");                
        delay(1000); 
        digitalWrite(LED, HIGH);     
        
      }
      else
      {        
        String text = "QR Read: FAIL";
      }
    }
  }
  
  cam.clearMemory(image_rgb);
}
