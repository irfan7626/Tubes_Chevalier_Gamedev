using UnityEngine;
using UnityEngine.UI; // Diperlukan untuk mengakses RawImage

public class FadeOutAndDisableRaycast : MonoBehaviour
{
    // Durasi animasi fade out dalam detik
    public float fadeDuration = 2.0f; 
    
    // Waktu tunda sebelum animasi dimulai
    public float delayBeforeFade = 1.0f;

    private RawImage rawImage;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
        // Panggil fungsi untuk memulai proses fade setelah jeda
        Invoke("StartFading", delayBeforeFade);
    }

    void StartFading()
    {
        // Memulai proses fade out
        StartCoroutine(FadeOut());
    }

    System.Collections.IEnumerator FadeOut()
    {
        float counter = 0;
        Color originalColor = rawImage.color;

        while (counter < fadeDuration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, counter / fadeDuration);
            rawImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // --- INI BAGIAN PALING PENTING ---
        // Setelah animasi selesai, matikan Raycast Target agar tidak menghalangi
        rawImage.raycastTarget = false;
        
        // Opsional: Nonaktifkan seluruh objek RawImage jika Anda mau
        // gameObject.SetActive(false);
    }
}