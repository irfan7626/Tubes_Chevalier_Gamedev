using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    private PuzzlePiece[] puzzlePieces;
    private bool puzzleSolved = false;

    // OnEnable berjalan setiap kali UI puzzle ini muncul
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

        // 1. Sembunyikan kembali UI Puzzle
        // gameObject merujuk ke PuzzleBoard itu sendiri
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