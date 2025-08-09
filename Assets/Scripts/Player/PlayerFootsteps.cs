using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip walkStep1;
    public AudioClip walkStep2;

    public float stepDelay = 0.5f; // Time before the first footstep sound plays
    public float stepInterval = 0.4f; // Time between footsteps while moving

    private float moveTimer = 0f;
    private float stepTimer = 0f;
    private bool isMoving = false;

    void Update()
    {
        // Detect if player is moving (WASD/Arrow keys)
        float move = Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical"));

        if (move > 0)
        {
            if (!isMoving)
            {
                isMoving = true;
                moveTimer = 0f; // reset timer when start moving
            }

            moveTimer += Time.deltaTime;

            // Only play footsteps after moving for stepDelay seconds
            if (moveTimer >= stepDelay)
            {
                stepTimer -= Time.deltaTime;
                if (stepTimer <= 0f)
                {
                    PlayFootstep();
                    stepTimer = stepInterval;
                }
            }
        }
        else
        {
            isMoving = false;
            moveTimer = 0f;
        }
    }

    void PlayFootstep()
    {
        AudioClip clip = Random.value < 0.5f ? walkStep1 : walkStep2;
        audioSource.PlayOneShot(clip);
    }
}