import cv2
from deepface import DeepFace
import numpy as np

# Code used from tutorial @ https://geekyhumans.com/emotion-detection-using-python-and-deepface/

face_cascade_name = cv2.data.haarcascades + 'haarcascade_frontalface_alt.xml'
face_cascade = cv2.CascadeClassifier()
if not face_cascade.load(cv2.samples.findFile(face_cascade_name)):
    print("Error loading xml file")

video = cv2.VideoCapture(0)

print(video)

#
# NOTE: This file expects you to be in the scripts folder of your unity project and
# that the scripts folder has a subfolder called "Resources"
#

with open("Resources/emotion.txt", "w+") as emotion_file:
    with open("Resources/emotion_log.txt", "a") as emotion_log:
        while True:
            _,frame = video.read()

            gray=cv2.cvtColor(frame,cv2.COLOR_BGR2GRAY)  
            face=face_cascade.detectMultiScale(gray,scaleFactor=1.1,minNeighbors=5)

            for x,y,w,h in face:
              try:
                  analyze = DeepFace.analyze(frame, actions=['emotion'])
                  out = (f"{analyze}......")
                  emotion_file.seek(0)
                  emotion_file.write(out)
                  emotion_file.truncate()
                  emotion_log.write(out)
                  print(out)
              except Exception as e:
                  print(e)

video.release()