using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    public void SetPositionCube(Vector3 newPosition)
    {
        transform.position += newPosition;
    }
}
