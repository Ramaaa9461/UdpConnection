using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

public class UdpConnection
{
    MessageReceiver messageReceiver;

    private struct DataReceived
    {
        public byte[] data;
        public IPEndPoint ipEndPoint;
    }


    private readonly UdpClient connection;
    private IReceiveData receiver = null;
    private Queue<DataReceived> dataReceivedQueue = new Queue<DataReceived>();

    object handler = new object();
    
    public UdpConnection(int port, IReceiveData receiver = null)
    {
        connection = new UdpClient(port);

        this.receiver = receiver;

        connection.BeginReceive(OnReceive, null);

        messageReceiver = new MessageReceiver();
    }

    public UdpConnection(IPAddress ip, int port, IReceiveData receiver = null)
    {
        connection = new UdpClient();
        connection.Connect(ip, port);

        this.receiver = receiver;

        connection.BeginReceive(OnReceive, null);
        
        messageReceiver = new MessageReceiver();
    }

    public void Close()
    {
        connection.Close();
    }

    public void FlushReceiveData()
    {
        lock (handler)
        {
            while (dataReceivedQueue.Count > 0)
            {
                DataReceived dataReceived = dataReceivedQueue.Dequeue();
                if (receiver != null)
                    receiver.OnReceiveData(dataReceived.data, dataReceived.ipEndPoint);
            }
        }
    }

    private void OnReceive(IAsyncResult ar)
    {
        try
        {
            DataReceived dataReceived = new DataReceived();
            dataReceived.data = connection.EndReceive(ar, ref dataReceived.ipEndPoint);

            lock (handler)
            {
                dataReceivedQueue.Enqueue(dataReceived);
            }

            NetVector3 vec = new NetVector3(UnityEngine.Vector3.right * 5);
            messageReceiver.NewMessage(vec.Serialize());
        }
        catch(SocketException e)
        {
            // This happens when a client disconnects, as we fail to send to that port.
            UnityEngine.Debug.LogError("[UdpConnection] " + e.Message);
        }

        connection.BeginReceive(OnReceive, null);
    }

    public void Send(byte[] data)
    {
        
        connection.Send(data, data.Length);
    }

    public void Send(byte[] data, IPEndPoint ipEndpoint)
    {
        connection.Send(data, data.Length, ipEndpoint);
    }

    public void Send(IMessage<UnityEngine.Vector3> data)
    {
        connection.Send(data.Serialize(), data.Serialize().Length);
    }

    public void Send(IMessage<UnityEngine.Vector3> data, IPEndPoint ipEndpoint)
    {
        connection.Send(data.Serialize(), data.Serialize().Length, ipEndpoint);
    }
}