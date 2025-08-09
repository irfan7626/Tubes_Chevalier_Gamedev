using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [Header("Settings")]
    public string requiredItemID = "Crowbar"; // Item yang dibutuhkan

    [Header("Feedback")]
    public AudioSource breakSound; // Suara papan hancur (opsional)

    // FUNGSI INI AKAN DIPANGGIL DARI SKRIP PEMAIN
    public void AttemptToBreak()
    {
        // Cek apakah pemain punya item yang dibutuhkan (linggis)
        if (InventoryManager.Instance.HasKey(requiredItemID))
        {
            Break();
        }
        else
        {
            Debug.Log("Butuh alat yang tepat untuk mendobrak ini.");
            // Di sini bisa ditambahkan suara "gagal"
        }
    }

    private void Break()
    {
        Debug.Log("Papan " + gameObject.name + " berhasil dihancurkan!");

        // Mainkan suara papan hancur jika ada
        if (breakSound != null)
        {
            breakSound.Play();
        }

        // Hancurkan objek papan ini
        Destroy(gameObject);
    }
}