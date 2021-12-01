using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public float range = 100f;
    public float damage = 10f;

    public int maxAmmo = 10;
    public int currentAmmo; // public to be modified by collision
    bool isAmmo;
    public int ammoCollected;
    public LayerMask ammoMask;
    public Collider[] ammoCollided;

    public Camera fpsCam;
    public TMPro.TextMeshProUGUI ammoDisplay;
    public ParticleSystem muzzleFlash;
    public AudioSource fireSFX; // Gunshot SFX used when shooting gun (by Videvo): https://www.videvo.net/sound-effect/gun-shot-single-shot-in-pe1097906/246309/

    void Start()
    {
        currentAmmo = maxAmmo;
        ammoDisplay.text = "Ammo: " + currentAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // true if collides with something in ammo layer
        isAmmo = Physics.CheckCapsule(fpsCam.transform.position + new Vector3(0, -1, 0), fpsCam.transform.position + new Vector3(0, 1, 0), 2f, ammoMask);

        // returns all things which collided with the given capsule
        ammoCollided = Physics.OverlapCapsule(fpsCam.transform.position + new Vector3(0, -1, 0), fpsCam.transform.position + new Vector3(0, 1, 0), 2f, ammoMask);

        if (isAmmo)
        {
            currentAmmo += ammoCollected;
            ammoDisplay.text = "Ammo: " + currentAmmo.ToString();
            for (int i = 0; i < ammoCollided.Length; i++)
            {
                Ammo ammoComponent = ammoCollided[i].GetComponent<Ammo>();
                ammoComponent.OnCollect();
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && currentAmmo >= 1)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        fireSFX.Play();
        currentAmmo--;

        ammoDisplay.text = "Ammo: " + currentAmmo.ToString();

        RaycastHit hit; // starting position, direction, the object the ray hits, range
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            // using the target class Target.cs
            Target target = hit.transform.GetComponent<Target>();

            // only if there is actually a target component
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            else
            {
                // checks if it is a door target instead
                DoorTarget doorTarget = hit.transform.GetComponent<DoorTarget>();
                if (doorTarget != null)
                {
                    doorTarget.TakeDamage(damage);
                }
            }
        }
    }
}
