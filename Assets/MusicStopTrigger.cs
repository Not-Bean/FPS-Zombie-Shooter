using UnityEngine;

public class MusicStopTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the Player has the "Player" tag
        {
            if (AudioManager.instance != null)
            {
                AudioManager.instance.StopMainMenuMusic();
                AudioManager.instance.PlayAmbienceAndMusic(); // Start main scene music
            }

            Destroy(gameObject); // Remove the trigger so it doesn't run again
        }
    }
}

