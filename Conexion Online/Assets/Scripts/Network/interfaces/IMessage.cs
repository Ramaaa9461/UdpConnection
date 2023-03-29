using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MessageType
{
    Console = 0,
    Position = 1
}

public interface IMessage<T> 
{
    public MessageType GetMessageType();
    
    public byte[] Serialize();

    public T Deserialize(byte[] message);

    public T getData();
}