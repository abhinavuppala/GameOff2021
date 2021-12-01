using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerInstructions : MonoBehaviour
{
    public Camera fpsCam;
    public LayerMask instructionMask;
    public TMPro.TextMeshProUGUI instructionsDisplay;

    string instructions;
    bool isRemoved = false;
    int timeToDisplay;
    bool isInstructions;
    Collider[] instructionsCollided;

    // Update is called once per frame
    void Update()
    {
        // true if collides with something in instructions layer
        isInstructions = Physics.CheckCapsule(fpsCam.transform.position + new Vector3(0, -1.5f, 0), fpsCam.transform.position + new Vector3(0, 1.5f, 0), 2f, instructionMask);

        if (isInstructions)
        {
            // returns all things which collided with the given capsule that are instructions
            instructionsCollided = Physics.OverlapCapsule(fpsCam.transform.position + new Vector3(0, -1.5f, 0), fpsCam.transform.position + new Vector3(0, 1.5f, 0), 2f, instructionMask);
            for (int i = 0; i < instructionsCollided.Length; i++)
            {
                // gets instructions from the thing it collided with
                instructions = instructionsCollided[i].GetComponent<Instructions>().instructions;
                timeToDisplay = instructionsCollided[i].GetComponent<Instructions>().seconds;

                // displays the instructions and removes them after a delay
                instructionsDisplay.text = instructions + "\n(Press the X key to continue)";
                instructionsCollided[i].GetComponent<Instructions>().Remove();
                Invoke("RemoveInstructions", timeToDisplay);
            }

        }

        // to skip the wait
        if (Input.GetKeyDown(KeyCode.X))
        {
            instructionsDisplay.text = "";
            isRemoved = true;
        }
    }

    void RemoveInstructions()
    {
        if (!isRemoved)
        {
            instructionsDisplay.text = "";
        }
    }
}
