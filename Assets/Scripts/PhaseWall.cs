using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseWall : MonoBehaviour
{
    public BoxCollider bc;
    
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider>();
    }

    public void EnablePhase()
    {
        bc.enabled = false;
    }

    public void DisablePhase()
    {
        bc.enabled = true;
    }
}
