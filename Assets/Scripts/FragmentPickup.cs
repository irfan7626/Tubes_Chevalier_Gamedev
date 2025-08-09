using UnityEngine;

public class FragmentPickup : MonoBehaviour
{
    [Header("Data Fragmen")]
    public string fragmentID; // ID unik, misal: "Fragmen1"
    public Sprite fragmentIcon; // Gambar ikon untuk ditampilkan di UI

    private void OnTriggerEnter(Collider other)
    {
        // DEBUG #1: Cek apakah ada sentuhan yang terdeteksi sama sekali
        Debug.Log("Trigger Fragmen disentuh oleh objek: " + other.name);

        // Cek apakah yang menyentuh adalah pemain
        if (other.CompareTag("Player"))
        {
            // DEBUG #2: Konfirmasi bahwa yang menyentuh adalah Player
            Debug.Log("Objek yang menyentuh adalah Player. Mencoba mengambil fragmen...");

            // Cek apakah fragmentID sudah diisi
            if (!string.IsNullOrEmpty(fragmentID))
            {
                // Panggil InventoryManager untuk menambahkan item
                InventoryManager.Instance.AddKey(fragmentID, fragmentIcon);

                // DEBUG #3: Konfirmasi bahwa item berhasil ditambahkan
                Debug.Log("Fragmen '" + fragmentID + "' berhasil ditambahkan ke inventory.");

                // Hancurkan objek fragmen ini setelah diambil
                Destroy(gameObject);
            }
            else
            {
                // DEBUG #4: Peringatan jika Fragment ID kosong
                Debug.LogWarning("GAGAL: Fragment ID pada objek " + gameObject.name + " belum diisi!");
            }
        }
    }
}