using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetHandShake : IMessage<(long, int)>
{

    (long, int) data;

    public NetHandShake((long, int) data)
    {
        this.data = data;
    }

    public NetHandShake(byte[] data)
    {
        this.data = Deserialize(data);
    }

    public (long, int) Deserialize(byte[] message)
    {
        (long, int) outData;

        outData.Item1 = BitConverter.ToInt64(message, 4);
        outData.Item2 = BitConverter.ToInt32(message, 12);

        return outData;
    }

    public (long, int) getData()
    {
        return data;
    }

    public MessageType GetMessageType()
    {
        return MessageType.HandShake;
    }

    public byte[] Serialize()
    {
        List<byte> outData = new List<byte>();

        outData.AddRange(BitConverter.GetBytes((int)GetMessageType()));

        outData.AddRange(BitConverter.GetBytes(data.Item1));
        outData.AddRange(BitConverter.GetBytes(data.Item2));

        return outData.ToArray();
    }

}
