using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;




public class MessageReceiver
{

    UnityEngine.Vector3 positionReceived;
    public MessageReceiver()
    { }

    public void NewMessage(byte[] message)
    {
        int messageType = 0;

        messageType = BitConverter.ToInt32(message, 0);

        switch ((MessageType)messageType)
        {
            case MessageType.Console:


                break;
            case MessageType.Position:

                NetVector3 netVector3 = new NetVector3(message);

                positionReceived = netVector3.getData();

                break;
            default:
                break;
        }
    }

}