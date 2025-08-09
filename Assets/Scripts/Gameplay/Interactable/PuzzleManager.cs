using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    // --- VARIABEL BARU: Seret objek kunci yang ingin dimunculkan ke sini ---
    public GameObject objectToRevealAfterSolve;

    private PuzzlePiece[] puzzlePieces;
    private bool puzzleSolved = false;

    void OnEnable()
    {
        puzzleSolved = false;
        puzzlePieces = GetComponentsInChildren<PuzzlePiece>();

        // Acak rotasi semua kepingan
        foreach (PuzzlePiece piece in puzzlePieces)
        {
            piece.RandomizeRotation();
        }
    }

    public void CheckWinCondition()
    {
        if (puzzleSolved) return;

        foreach (PuzzlePiece piece in puzzlePieces)
        {
            if (!piece.isCorrect)
            {
                return; // Jika ada satu saja yg salah, keluar dari fungsi
            }
        }

        // Jika loop selesai, artinya semua benar
        OnPuzzleSolved();
    }

    void OnPuzzleSolved()
    {
        puzzleSolved = true;
        Debug.Log("PUZZLE SELESAI!");

        // --- BAGIAN BARU: Logika untuk memunculkan kunci ---
        if (objectToRevealAfterSolve != null)
        {
            objectToRevealAfterSolve.SetActive(true); // Aktifkan objeknya
            Debug.Log(objectToRevealAfterSolve.name + " telah muncul!");
        }
        // ----------------------------------------------------

        // 1. Sembunyikan kembali UI Puzzle
        gameObject.SetActive(false);

        // 2. Kunci dan sembunyikan kursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 3. Aktifkan kembali gerakan pemain
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var playerMovement = player.GetComponent<Playermovement2>();
            if (playerMovement != null)
            {
                playerMovement.enabled = true;
            }
        }
    }
}