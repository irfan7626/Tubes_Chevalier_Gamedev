using UnityEngine;

public class LockedInteraction : MonoBehaviour
{
    public AudioClip padlockSound; // Suara padlock (MP3/WAV)
    public float volume = 1f;

    private AudioSource audioSource;

    void Start()
    {
        // Tambahkan AudioSource ke objek ini
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void TryInteract()
    {
        // Misalnya ini pintu terkunci
        bool isLocked = true;

        if (isLocked)
        {
            PlayPadlockSound();
            Debug.Log("Pintu terkunci!");
        }
        else
        {
            // Kalau tidak terkunci, jalankan logika membuka pintu
            Debug.Log("Pintu terbuka.");
        }
    }

    void PlayPadlockSound()
    {
        if (padlockSound != null)
        {
            audioSource.PlayOneShot(padlockSound, volume);
        }
    }
}