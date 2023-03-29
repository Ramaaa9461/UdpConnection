using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MessageReceiver : MonoBehaviour
{
    public UnityEvent<Vector3> newPosition;

    public MessageReceiver()
    { }
    
    public void NewMessage(byte[] message)
    {
        int messageType;

        messageType = BitConverter.ToInt32(message, 0);

        switch ((MessageType)messageType)
        {
            case MessageType.Console:


                break;
            case MessageType.Position:

                NetVector3 netVector3 = new NetVector3(message);
                newPosition.Invoke(netVector3.getData());

                break;
            default:
                break;
        }
    }



}
