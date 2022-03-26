import socket
import time

host, port = "127.0.0.1", 8080
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

try:
    sock.connect((host, port))
    sock.sendall("this is a cool string im sending".encode('utf-8'))
    time.sleep(1)
    sock.sendall("stop".encode('utf-8'))
finally:
    sock.close()