using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;

/* 
THIS CODE IS INSPIRED FROM THE WONDERFUL GITHUB OF ConorZAM
https://github.com/ConorZAM/Python-Unity-Socket/blob/master/README.md
*/

public class NetworkReceiver : MonoBehaviour {
    Thread mThread;
    public string connectionIP = "127.0.0.1";
    public int connectionPort = 8080;
    public string receivedString;
    IPAddress localAdd;
    TcpListener listener;
    TcpClient client;
    bool running;
    

    private void Start()
    {
        // Receive on a separate thread so Unity doesn't freeze waiting for data
        ThreadStart ts = new ThreadStart(GetInfo);
        mThread = new Thread(ts);
        mThread.Start();
    }

    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }
    
    void GetInfo()
    {
        localAdd = IPAddress.Parse(connectionIP);
        listener = new TcpListener(IPAddress.Any, connectionPort);
        listener.Start();

        client = listener.AcceptTcpClient();

        running = true;

        while (running) {
            Connection();
        }

        listener.Stop();
    }

    void Connection()
    {
        Debug.Log("Starting...");
        NetworkStream nwStream = client.GetStream();
        byte[] buffer = new byte[client.ReceiveBufferSize];

        int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);
        // Passing data as strings, not ideal but easy to use
        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        if (dataReceived != null)
        {
            Debug.Log(dataReceived);
            if (dataReceived == "stop")
            {
            // Can send a string "stop" to kill the connection
                Debug.Log("Stopping.");
                running = false;
            }
            else
            {
                // Convert the received string of data to the format we are using
                this.receivedString = dataReceived;
                Debug.Log(dataReceived);
                // nwStream.Write(buffer, 0, bytesRead);
            }
        }
    }

}