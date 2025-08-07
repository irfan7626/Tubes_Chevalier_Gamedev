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

    // --- VARIABEL BARU UNTUK TANGGA ---
    public float climbSpeed = 5f;
    private bool onLadder = false;
    // ------------------------------------

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameraStandingPos = playerCamera.transform.localPosition;
        cameraCrouchPos = cameraStandingPos - new Vector3(0, 0.5f, 0);
    }

    void Update()
    {
        // === BAGIAN BARU: Cek apakah sedang di tangga ===
        if (onLadder)
        {
            // Jika di tangga, jalankan logika memanjat
            HandleLadderMovement();
        }
        else
        {
            // Jika tidak, jalankan logika gerakan normal
            HandleNormalMovement();
        }
        // ===============================================

        // Rotasi kamera dan pemain tetap berjalan di kedua mode
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
    
    // === FUNGSI BARU: Untuk logika memanjat tangga ===
    void HandleLadderMovement()
    {
        // Hentikan gravitasi saat di tangga
        moveDirection = Vector3.zero;

        // Ambil input vertikal (W/S) untuk naik/turun
        float verticalInput = Input.GetAxis("Vertical");
        
        // Gerakkan controller ke atas atau ke bawah
        characterController.Move(transform.up * verticalInput * climbSpeed * Time.deltaTime);
    }
    // ===============================================

    // === KODE LAMA ANDA: Dibungkus dalam satu fungsi agar rapi ===
    void HandleNormalMovement()
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
    // ==========================================================

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

    // --- FUNGSI BARU: Untuk mendeteksi kapan pemain menyentuh trigger tangga ---
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            onLadder = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            onLadder = false;
        }
    }
    // -------------------------------------------------------------------------
}