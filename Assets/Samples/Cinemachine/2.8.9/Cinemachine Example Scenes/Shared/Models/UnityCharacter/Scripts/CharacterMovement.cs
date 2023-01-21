using UnityEngine;
using System.Collections;

namespace Cinemachine.Examples
{

[AddComponentMenu("")] // Don't display in add component menu
public class CharacterMovement : MonoBehaviour
{
    public bool useCharacterForward = false;
    public bool lockToCameraForward = false;
    public float turnSpeed = 10f;
    public KeyCode sprintJoystick = KeyCode.JoystickButton2;
    public KeyCode sprintKeyboard = KeyCode.LeftShift;
    public KeyCode jumpJoystick = KeyCode.JoystickButton0;
    public KeyCode jumpKeyboard = KeyCode.Space;
    public float jumpForce = 5.0f;
    public float sprintingMultiplier = 10f;
    public Vector3 respawnPoint;
    private AudioSource audioSource;
    private float turnSpeedMultiplier;
    private float speed = 0f;
    private float direction = 0f;
    private bool isSprinting = false;
    private Animator anim;
    private Vector3 targetDirection;
    private Vector2 input;
    private Quaternion freeRotation;
    private Camera mainCamera;
    private float velocity;
    private bool isGrounded = false;
    private Vector3 jump;
    private Rigidbody rb;
    private bool isLocked;
    public AudioClip deathSound;
    public AudioClip jumpSound;
    public AudioClip landSound;
    //list of jump sounds
    public AudioClip[] jumpSounds;
    public Animator animator;
    // private bool isJumping = false;
	// Use this for initialization
	void Start ()
	{
	    // anim = GetComponent<Animator>();
	    mainCamera = Camera.main;
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        rb = GetComponent<Rigidbody>();
        respawnPoint = transform.position;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        
	}
    public void SetLock(bool newLocked)
    {
        isLocked = newLocked;
    }
    public void Respawn(float delay)
    {
        StartCoroutine(RespawnCoroutine(delay));
    }
    private IEnumerator RespawnCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.position = respawnPoint;
    }
    void Update()
    {
        // if(isLocked)
        //     return;
        if (Input.GetKeyDown(jumpJoystick) || Input.GetKeyDown(jumpKeyboard))
        {
            if (isGrounded)
            {
                // isJumping = true;
                rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                audioSource.Stop();
                audioSource.PlayOneShot(jumpSounds[Random.Range(0, jumpSounds.Length)]);

                isGrounded = false;
            }
        }
    }
	// Update is called once per frame
	void FixedUpdate ()
	{
        // if(isLocked)
        //     return;
	    input.x = Input.GetAxis("Horizontal");
	    input.y = Input.GetAxis("Vertical");

		// set speed to both vertical and horizontal inputs
        if (useCharacterForward)
            speed = Mathf.Abs(input.x) + input.y;
        else
            speed = Mathf.Abs(input.x) + Mathf.Abs(input.y);

        // speed = Mathf.Clamp(speed, 0f, 1f);
        // transform.position += transform.forward * speed * Time.deltaTime * 5f;
        // speed = Mathf.SmoothDamp(anim.GetFloat("Speed"), speed, ref velocity, 0.1f);
        // anim.SetFloat("Speed", speed);
        // set sprinting
	    isSprinting = ((Input.GetKey(sprintJoystick) || Input.GetKey(sprintKeyboard)) && input != Vector2.zero && direction >= 0f);
        if(isSprinting)
            transform.position += transform.forward * speed * Time.deltaTime * 10f;
        else
            transform.position += transform.forward * speed * Time.deltaTime * 1f;
        // anim.SetBool("isSprinting", isSprinting);

	    if (input.y < 0f && useCharacterForward)
            direction = input.y;
	    else
            direction = 0f;

        // anim.SetFloat("Direction", direction);


        // Update target direction relative to the camera view (or not if the Keep Direction option is checked)
        UpdateTargetDirection();
        if (input != Vector2.zero && targetDirection.magnitude > 0.1f)
        {
            Vector3 lookDirection = targetDirection.normalized;
            freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
            var diferenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
            var eulerY = transform.eulerAngles.y;

            if (diferenceRotation < 0 || diferenceRotation > 0) eulerY = freeRotation.eulerAngles.y;
            var euler = new Vector3(0, eulerY, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), turnSpeed * turnSpeedMultiplier * Time.deltaTime);
        }
        // Debug.Log("isGrounded: " + isGrounded);
        //jump
        // if (isJumping)
        // {
        //     rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        //     isJumping = false;
        //     isGrounded = false;
        // }
	}
    // private void OnCollisionEnter(Collision other) {
    //     Debug.Log("Collision: " + other.gameObject.tag);
    //     if(other.gameObject.tag == "Water")
    //     {
    //         // audioSource.Stop();
    //         // audioSource.PlayOneShot(deathSound);
    //         transform.position = respawnPoint;
    //     }
    // }
    public virtual void UpdateTargetDirection()
    {
        if (!useCharacterForward)
        {
            turnSpeedMultiplier = 1f;
            var forward = mainCamera.transform.TransformDirection(Vector3.forward);
            forward.y = 0;

            //get the right-facing direction of the referenceTransform
            var right = mainCamera.transform.TransformDirection(Vector3.right);

            // determine the direction the player will face based on input and the referenceTransform's right and forward directions
            targetDirection = input.x * right + input.y * forward;
        }
        else
        {
            turnSpeedMultiplier = 0.2f;
            var forward = transform.TransformDirection(Vector3.forward);
            forward.y = 0;

            //get the right-facing direction of the referenceTransform
            var right = transform.TransformDirection(Vector3.right);
            targetDirection = input.x * right + Mathf.Abs(input.y) * forward;
        }
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Ground")
            isGrounded = true;
    }
    void OnCollisionExit()
    {
        isGrounded = false;
    }
}
}
