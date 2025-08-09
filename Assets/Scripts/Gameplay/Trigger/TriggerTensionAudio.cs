using UnityEngine;

public class TensionTrigger : MonoBehaviour
{
    [Header("Audio Tension")]
    public AudioClip tensionClip; // drag audio file di sini
    private AudioSource audioSource;

    private bool hasPlayed = false;

    void Start()
    {
        // Tambah komponen AudioSource otomatis
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasPlayed) return; // Biar cuma sekali main

        if (other.CompareTag("Player"))
        {
            audioSource.clip = tensionClip;
            audioSource.Play();
            hasPlayed = true;
            Debug.Log("Tension audio dimulai!");
        }
    }
}