using UnityEngine;

public class PhotoPuzzleController : MonoBehaviour
{
    [Header("Puzzle Interaction")]
    public GameObject triggerObject;          // Objek pemicu (misal meja puzzle)
    public GameObject player;                 // Player First Person
    public Camera puzzleCamera;               // Kamera khusus puzzle (opsional)
    public GameObject puzzleUI;               // Objek UI/board puzzle

    [Header("Puzzle Pieces")]
    public GameObject[] puzzlePieces;         // Potongan gambar
    public Transform[] targetPositions;       // Posisi target masing-masing puzzle

    [Header("Drag Settings")]
    public float dragDistance = 1.5f;         // Jarak maksimum bisa ditarik
    public float snapDistance = 0.5f;         // Jarak untuk snap

    private bool isPuzzleActive = false;
    private bool isPlayerNearby = false;
    private bool[] isPiecePlaced;
    private int correctPlaced = 0;

    void Start()
    {
        if (puzzleUI != null) puzzleUI.SetActive(false);
        if (puzzleCamera != null) puzzleCamera.enabled = false;

        isPiecePlaced = new bool[puzzlePieces.Length];
    }

    void Update()
    {
        if (isPlayerNearby && !isPuzzleActive && Input.GetKeyDown(KeyCode.E))
        {
            StartPuzzle();
        }

        if (isPuzzleActive)
        {
            HandleDragging();
        }
    }

    void StartPuzzle()
    {
        isPuzzleActive = true;

        if (puzzleUI != null) puzzleUI.SetActive(true);
        if (puzzleCamera != null) puzzleCamera.enabled = true;
        if (player != null) player.GetComponent<FPSPlayerController>().enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void EndPuzzle()
    {
        isPuzzleActive = false;

        if (puzzleUI != null) puzzleUI.SetActive(false);
        if (puzzleCamera != null) puzzleCamera.enabled = false;
        if (player != null) player.GetComponent<FPSPlayerController>().enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Puzzle selesai!");
    }

    void HandleDragging()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, 0);

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 point = ray.GetPoint(distance);

                for (int i = 0; i < puzzlePieces.Length; i++)
                {
                    if (isPiecePlaced[i]) continue;

                    if (Vector3.Distance(puzzlePieces[i].transform.position, point) < dragDistance)
                    {
                        puzzlePieces[i].transform.position = new Vector3(point.x, targetPositions[i].position.y, point.z);

                        if (Vector3.Distance(puzzlePieces[i].transform.position, targetPositions[i].position) < snapDistance)
                        {
                            puzzlePieces[i].transform.position = targetPositions[i].position;
                            puzzlePieces[i].transform.rotation = targetPositions[i].rotation;

                            isPiecePlaced[i] = true;
                            correctPlaced++;

                            if (correctPlaced >= puzzlePieces.Length)
                            {
                                EndPuzzle();
                            }
                        }
                        break;
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerNearby = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerNearby = false;
    }
}
