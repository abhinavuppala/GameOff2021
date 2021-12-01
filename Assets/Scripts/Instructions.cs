using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    // object with this class must be in the instructions layer to function
    
    // stores the instructions to be displayed on contact with the player and how long to display them for
    public string instructions = "[TYPE INSTRUCTIONS HERE]";
    public int seconds = 5;

    public void Remove()
    {
        Destroy(gameObject);
    }
}
