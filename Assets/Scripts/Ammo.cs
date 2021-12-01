using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public ParticleSystem collectParticles;

    // make sure functions to be used outside are public
    public void OnCollect()
    {
        ParticleSystem collect = Instantiate(collectParticles, transform.position, transform.rotation, null);
        collect.Play();
        Destroy(gameObject);
    }
}
