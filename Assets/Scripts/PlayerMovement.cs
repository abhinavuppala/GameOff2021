using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // references
    public CharacterController controller;
    public TMPro.TextMeshProUGUI ammoDisplay;
    public Transform groundCheck;
    public Camera fpsCam;
    public int currentAmmo;
    public LayerMask groundMask;
    public LayerMask phaseMask;
    public LayerMask finishMask;
    public LayerMask deadlyMask;
    public AudioSource phase; // Free glitch SFXs used for glitch powerups (by Rocketstock): https://www.rocketstock.com/free-after-effects-templates/10-free-glitch-sfx-for-video/
    public AudioSource lagback;

    // keybinds, constants
    public KeyCode lagbackKeybind = KeyCode.G;
    public KeyCode sprintKeybind = KeyCode.LeftShift;
    public KeyCode phaseKeybind = KeyCode.T;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float range = 3f;

    // speed is default speed, sprint is how much sprinting speeds you up
    public float speed = 14f;
    public float sprint = 5f;
    public float jumpHeight = 5f;

    // more references
    Vector3 velocity;
    float moveSpeed = 0f;
    bool isGrounded;
    Vector3 lastGroundedPos;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // quit if escape is pressed
            Application.Quit();
        }
        
        // reload scene if R is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }

        // checks if touching finish, ground, or deadly, and acts accordingly
        CheckFinish();
        CheckDeadly();
        CheckGround();

        // moves player, along with sprint
        CheckSprint();
        MovePlayer();

        // phases through wall if pressed (and if next to & looking at phaseable wall)
        if (Input.GetKeyDown(phaseKeybind))
        {
            Phase();
        }

        // lagback, overriding all other movement if clicked
        if (Input.GetKeyDown(lagbackKeybind) && !isGrounded)
        {
            Lagback();
        }
    }

    private void CheckSprint()
    {
        // GetKey - true if key is pressed currently
        // GetKeyDown - true only when the key is pressed
        moveSpeed = speed;
        if (Input.GetKey(sprintKeybind))
        {
            moveSpeed += 5;
        }
    }

    private void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // takes into account player's rotation
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * moveSpeed * Time.deltaTime);

        // v = sqrt(-2 * h * g)
        // v: velocity needed to jump h high
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // decrease velocity
        velocity.y += gravity * Time.deltaTime;

        // y = (1/2)g(t^2) according to physics
        // that is why it is again multiplied by Time.deltaTime
        controller.Move(velocity * Time.deltaTime);
    }

    private void CheckGround()
    {
        // true if collides with anything in the groundMask (makes sphere radius of groundDistance)
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded)
        {
            // save last position on ground for lagback ability
            lastGroundedPos = transform.position;
            if (velocity.y < 0) { velocity.y = 0f; }
        }
    }

    private void Lagback()
    {
        lagback.Play();
        // move back to the last grounded position if in air when pressed
        controller.Move(lastGroundedPos - transform.position);
        velocity = Vector3.zero;
    }

    private void Phase()
    {
        // use raycast to check if looking at phaseable wall, then phase through if that is the case
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            PhaseWall pw = hit.transform.GetComponent<PhaseWall>();

            // if the thing hit has a phaseWall component
            if (pw != null)
            {
                pw.EnablePhase();
                phase.Play();
                controller.Move(fpsCam.transform.forward * 2 * range);
                pw.DisablePhase();
            }
        }
    }

    private void CheckDeadly()
    {
        bool isDeadly = Physics.CheckCapsule(fpsCam.transform.position + new Vector3(0, -2, 0), fpsCam.transform.position + new Vector3(0, 1, 0), 2f, deadlyMask);
        if (isDeadly)
        {
            ReloadScene();
        }
    }

    private void CheckFinish()
    {
        bool isFinish = Physics.CheckCapsule(fpsCam.transform.position + new Vector3(0, -1, 0), fpsCam.transform.position + new Vector3(0, 1, 0), 2f, finishMask);

        if (isFinish)
        {
            LoadNextScene();
        }
    }

    private static void LoadNextScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int totalScenes = SceneManager.sceneCountInBuildSettings;
        if (currentScene.buildIndex + 1 == totalScenes)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }
    }

    private static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
