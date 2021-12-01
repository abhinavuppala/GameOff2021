using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCollect : MonoBehaviour
{
    public int ammoCollected = 1;
    public Text ammoDisplay;

    void OnCollisionEnter(Collision collision)
    {
        // gets the ammo component of the collided (if at all) and the current ammo from the Gun component of the gun
        Ammo ammoComponent = collision.transform.GetComponent<Ammo>();
        int currentAmmo = gameObject.GetComponentInChildren<Gun>().currentAmmo;

        // if the player collides with ammo
        if (ammoComponent != null)
        {
            currentAmmo += ammoCollected;
            ammoDisplay.text = "Ammo: " + currentAmmo.ToString();
        }
    }
}
