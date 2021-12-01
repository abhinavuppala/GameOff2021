using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTDoor : MonoBehaviour
{
    public int targets = 0;
    public int targetsNeeded = 3;
    public float speed = 0.05f;

    // Update is called once per frame
    void Update()
    {
        // opens door if sufficient amount of targets are destroyed
        if (targets == targetsNeeded)
        {
            StartCoroutine("OpenDoor");
        }
    }

    public float height;
    IEnumerator OpenDoor()
    {
        for (float dist = 0; dist < height; dist += speed)
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
        for (float dist = 0; dist < height; dist += speed)
        {
            transform.Translate(0, speed, 0);
            yield return null;
        }
    }
}
