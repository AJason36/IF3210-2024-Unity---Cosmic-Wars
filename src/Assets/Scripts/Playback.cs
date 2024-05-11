using UnityEngine;

public class Playback: MonoBehaviour
{
    // Adjust this value to set the desired playback speed
    public float playbackSpeed = 0.7f;

    void Update()
    {
        // Set the time scale of the game to adjust playback speed
        Time.timeScale = playbackSpeed;
    }
}