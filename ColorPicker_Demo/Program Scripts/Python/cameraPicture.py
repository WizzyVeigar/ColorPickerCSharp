import picamera     # Importing the library for camera module
from time import sleep
print("script Started")
camera = picamera.PiCamera()# Setting up the camera
camera.resolution = (1024,980)
camera.brightness = 55
camera.contrast = 90
camera.rotation = 180
camera.start_preview()
sleep(5)
camera.capture('/home/pi/images/newImage.png') # Capturing the image
camera.stop_preview()
print('Done python')