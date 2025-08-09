using UnityEngine;

public class PickupItems : MonoBehaviour
{
    public AudioClip pickupSound; // Suara saat item diambil
    public float interactDistance = 3f; // Jarak interaksi
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Cek jarak pemain ke item
        if (Vector3.Distance(player.position, transform.position) <= interactDistance)
        {
            // Tekan E untuk ambil item
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (pickupSound != null)
                    AudioSource.PlayClipAtPoint(pickupSound, transform.position);

                Destroy(gameObject); // Hapus item dari scene
            }
        }
    }
}