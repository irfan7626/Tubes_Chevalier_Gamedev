using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public AudioClip pickupClip; // Masukkan suara pickup di Inspector
    public float pickupVolume = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Mainkan suara pickup di posisi item
            AudioSource.PlayClipAtPoint(pickupClip, transform.position, pickupVolume);

            // Hapus item setelah diambil
            Destroy(gameObject);
        }
    }
}