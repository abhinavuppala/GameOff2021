using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float height;
    public float speed = 0.1f;
    
    public void Open()
    {
        StartCoroutine("OpenDoor");
    }

    IEnumerator OpenDoor()
    {
        for (float dist = 0; dist < height; dist+=speed)
        {
            transform.Translate(0, -speed, 0);
            yield return null;
        }
    }

    public void Close()
    {
        StartCoroutine("CloseDoor");
    }

    IEnumerator CloseDoor()
    {
        for (float dist = 0; dist < height; dist+=speed)
        {
            transform.Translate(0, speed, 0);
            yield return null;
        }
    }
}
