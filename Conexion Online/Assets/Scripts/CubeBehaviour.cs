using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            MoveCube(speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            MoveCube(-speed);
        }

    }

    void MoveCube(float speed)
    {
         transform.position += Vector3.forward * speed * Time.deltaTime;

        NetVector3 netVector3 = new NetVector3(transform.position);

        if (NetworkManager.Instance.isServer)
        {
            NetworkManager.Instance.Broadcast(netVector3.Serialize());
            Debug.Log("isServer");
        }
        else
        {
            NetworkManager.Instance.SendToServer(netVector3.Serialize());
            Debug.Log("isNotServer");
        }
    }


 
}
