using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    public Vector3 range = new Vector3(5, 0, 0);
    public Vector3 center;
    public float speedMultiplier = 1f;

    void Start()
    {
        // gets starting position (to center the oscillation)
        center = transform.position;    
    }

    void Update()
    {
        // uses sine function to oscillate from one end of the range to another, back to back
        transform.SetPositionAndRotation(center + (range * Mathf.Sin(Time.time * speedMultiplier)), Quaternion.identity);
    }
}
