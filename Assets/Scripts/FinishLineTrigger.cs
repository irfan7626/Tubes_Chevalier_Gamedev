using UnityEngine;

 public class FinishLineTrigger : MonoBehaviour
 {
     public MazeTimer mazeTimerScript;
     private Playermovement2 playerMovementScript; // Asumsi nama skrip pergerakan pemain

     private void OnTriggerEnter(Collider other)
     {
         if (other.CompareTag("Player"))
         {
             // Hentikan timer
             if (mazeTimerScript != null)
             {
                 mazeTimerScript.StopTimer();
             }

             // Nonaktifkan pergerakan pemain
             playerMovementScript = other.GetComponent<Playermovement2>();
             if (playerMovementScript != null)
             {
                 playerMovementScript.enabled = false;
             }
             else
             {
                 Debug.LogWarning("Skrip Playermovement2 tidak ditemukan pada player!");
             }

             // Nonaktifkan trigger ini
             gameObject.SetActive(false);
         }
     }
 }