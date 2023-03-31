using System;
using System.Net;

public class MessageReceiver 
{
    UnityEngine.Vector3 vectorPosition;
    byte[] message;

    public MessageReceiver() { }

    public void NewMessage(byte[] message)
    {
        int messageType = 0;

        messageType = BitConverter.ToInt32(message, 0);
        this.message = message;

        switch ((MessageType)messageType)
        {
            case MessageType.Console:


                break;
            case MessageType.Position:

                NetVector3 netVector3 = new NetVector3(message);

                vectorPosition = netVector3.getData();


                //De alguna manera le tengo que mandar la posicion al cubito

                break;

            case MessageType.HandShake:

                NetHandShake netHandShake = new NetHandShake(message);

                NetworkManager.Instance.AddClient(new IPEndPoint(netHandShake.getData().Item1, netHandShake.getData().Item2));

                break;

            default:
                break;
        }
    }
}