using UnityEngine;
using UnityEngine.EventSystems; // Diperlukan untuk mendeteksi klik pada UI

public class PuzzlePiece : MonoBehaviour, IPointerClickHandler
{
    // Variabel untuk mengecek apakah rotasi kepingan ini sudah benar
    [HideInInspector] public bool isCorrect = false;

    // Fungsi ini akan otomatis berjalan saat objek UI ini diklik
    public void OnPointerClick(PointerEventData eventData)
    {
        // Putar kepingan sebesar 90 derajat berlawanan arah jarum jam
        transform.Rotate(0, 0, -90);

        // Setelah diputar, cek apakah posisinya sudah benar (menghadap ke atas)
        // Kita gunakan toleransi kecil (1.0f) karena perhitungan float tidak selalu presisi
        if (Mathf.Abs(transform.eulerAngles.z) < 1.0f)
        {
            isCorrect = true;
        }
        else
        {
            isCorrect = false;
        }
        
        // Panggil manager untuk mengecek kondisi kemenangan setiap kali ada gerakan
        transform.parent.GetComponent<PuzzleManager>().CheckWinCondition();
    }

    // Fungsi untuk mengacak rotasi di awal permainan
    public void RandomizeRotation()
    {
        int randomTurns = Random.Range(1, 4); // Acak putaran antara 1 sampai 3 kali
        for (int i = 0; i < randomTurns; i++)
        {
            transform.Rotate(0, 0, -90);
        }

        // Update status awal setelah diacak
        if (Mathf.Abs(transform.eulerAngles.z) < 1.0f)
        {
            isCorrect = true;
        }
        else
        {
            isCorrect = false;
        }
    }
}