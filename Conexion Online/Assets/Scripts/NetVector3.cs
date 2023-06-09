using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetVector3 : IMessage<Vector3>
{
    static int lastmsgID = 0;
    Vector3 data;
    public NetVector3(Vector3 data)
    {
        this.data = data;
    }

    public NetVector3(byte[] data)
    {
        this.data = Deserialize(data);
    }

    public Vector3 Deserialize(byte[] message)
    {
        Vector3 outData;

        outData.x = BitConverter.ToSingle(message, 4);
        outData.y = BitConverter.ToSingle(message, 8);
        outData.z = BitConverter.ToSingle(message, 12);

        return outData;
    }

    public MessageType GetMessageType()
    {
        return MessageType.Position;
    }

    public byte[] Serialize()
    {
        List<byte> outData = new List<byte>();

        outData.AddRange(BitConverter.GetBytes((int)GetMessageType()));
    //  outData.AddRange(BitConverter.GetBytes(lastmsgID++));

        outData.AddRange(BitConverter.GetBytes(data.x));
        outData.AddRange(BitConverter.GetBytes(data.y));
        outData.AddRange(BitConverter.GetBytes(data.z));

        return outData.ToArray();
    }

    public Vector3 getData()
    {
        return data;
    }

}
