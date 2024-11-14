using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public RuntimeAnimatorController controller;
    public CinemachineCamera playerCam;
    public Transform playerTransform;
    public CharacterController characterController;


    Vector3 velocity;

    const float walkSpeed = 30f;
    const float dashModifyer =1.5f;

    private float speed = walkSpeed;
    private float rotationSpeed = 180f;
    private string currAnimation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        controller = animator.runtimeAnimatorController;
        playerTransform = GetComponent<Transform>();
        currAnimation = "Idle";
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        playerCam.transform.Rotate(Input.GetAxis("Look Vertical") * rotationSpeed * Time.deltaTime, Input.GetAxis("Look Horizontal") * rotationSpeed * Time.deltaTime, 0);
        move *= speed * Time.deltaTime;  // Applying speed and delta time here

        characterController.Move(move);  // Move the character based on calculated movement vector
    }

    private void FixedUpdate()
    {
        float currentSpeed = (Input.GetKey(KeyCode.LeftShift)) ? walkSpeed * dashModifyer : walkSpeed;
        speed = currentSpeed;

        if (Input.GetAxis("Horizontal") > 0)
        {
            setAnimation("RightStrafeW");
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            setAnimation("LeftStrafeW");
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            setAnimation("Walk");
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            setAnimation("WalkBackwards");
        }
        else
        {
            setAnimation("Idle");
        }
    }

    private void setAnimation(string animName)
    {
        string prevAnimation = currAnimation;
        currAnimation = animName;
        animator.SetBool(prevAnimation, false);
        animator.SetBool(animName, true);
    }
}
