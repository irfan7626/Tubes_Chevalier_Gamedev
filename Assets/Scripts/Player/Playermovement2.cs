using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Playermovement2 : MonoBehaviour
{
    // Variabel Gerakan Normal (Tidak Diubah)
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float crouchSpeed = 3.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    // Variabel Crouch (Tidak Diubah)
    public float crouchHeight = 1.0f;
    public float standingHeight = 2.0f;
    public Vector3 cameraStandingPos;
    public Vector3 cameraCrouchPos;
    private bool isCrouching = false;

    // Variabel Tangga (Tidak Diubah)
    public float climbSpeed = 5f;
    private bool onLadder = false;

    // --- VARIABEL BARU UNTUK SUARA LANGKAH KAKI ---
    public AudioClip footstepSound;
    private AudioSource audioSource;
    // ---------------------------------------------

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameraStandingPos = playerCamera.transform.localPosition;
        cameraCrouchPos = cameraStandingPos - new Vector3(0, 0.5f, 0);

        // --- TAMBAHAN: Dapatkan komponen AudioSource dan siapkan klipnya ---
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.clip = footstepSound;
        }
        // -----------------------------------------------------------------
    }

    void Update()
    {
        if (onLadder)
        {
            HandleLadderMovement();
        }
        else
        {
            HandleNormalMovement();
        }

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        // --- TAMBAHAN: Panggil fungsi untuk mengelola suara langkah kaki ---
        HandleFootstepSounds();
        // -------------------------------------------------------------
    }

    // --- FUNGSI BARU: Untuk mengelola suara langkah kaki ---
    void HandleFootstepSounds()
    {
        // Cek apakah pemain di darat, sedang bergerak, dan tidak di tangga
        bool isMovingOnGround = characterController.isGrounded && !onLadder && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0);

        if (isMovingOnGround && canMove)
        {
            // Jika audio belum diputar, putar sekarang
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // Jika pemain diam, di udara, atau di tangga, hentikan suara
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
    // ------------------------------------------------------

    void HandleLadderMovement() // Fungsi ini tidak diubah
    {
        moveDirection = Vector3.zero;
        float verticalInput = Input.GetAxis("Vertical");
        characterController.Move(transform.up * verticalInput * climbSpeed * Time.deltaTime);
    }

    void HandleNormalMovement() // Fungsi ini tidak diubah
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float baseSpeed = isCrouching ? crouchSpeed : (isRunning ? runningSpeed : walkingSpeed);
        float curSpeedX = canMove ? baseSpeed * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? baseSpeed * Input.GetAxis("Horizontal") : 0;

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);
        HandleCrouch();
    }

    void HandleCrouch() // Fungsi ini tidak diubah
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
            characterController.height = crouchHeight;
            playerCamera.transform.localPosition = cameraCrouchPos;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
            characterController.height = standingHeight;
            playerCamera.transform.localPosition = cameraStandingPos;
        }
    }

    private void OnTriggerEnter(Collider other) // Fungsi ini tidak diubah
    {
        if (other.CompareTag("Ladder"))
        {
            onLadder = true;
        }
    }

    private void OnTriggerExit(Collider other) // Fungsi ini tidak diubah
    {
        if (other.CompareTag("Ladder"))
        {
            onLadder = false;
        }
    }
}