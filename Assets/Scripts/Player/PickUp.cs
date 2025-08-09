using UnityEngine;

public class PickupInteract : MonoBehaviour
{
    public AudioClip pickupClip; // Suara pickup
    public float pickupVolume = 1f;
    public float interactDistance = 3f; // Jarak interaksi

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= interactDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Mainkan suara pickup
                AudioSource.PlayClipAtPoint(pickupClip, transform.position, pickupVolume);

                // Hapus item
                Destroy(gameObject);
            }
        }
    }
}