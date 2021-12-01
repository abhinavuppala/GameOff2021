using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Credit to Brackeys
// https://www.youtube.com/watch?v=_QajrabyTJc&t=952s

public class PlayerLook : MonoBehaviour
{
    public float mouseSens = 100f;
    public Transform playerBody;

    float xRotation = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        // lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        // rotate only the camera on the vertical
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // rotate the full body on the horizontal
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
