using UnityEngine;
using UnityEngine.Playables;

public class CutSceneController : MonoBehaviour
{
    public PlayableDirector director;

    void Update()
    {
        // Check if the "C" key is pressed down
        if (Input.GetButtonDown("Skill"))
        {
            PlayTimeline();
        }
    }

    public void PlayTimeline()
    {
        if (director != null)
        {
            director.Play();
            Debug.Log("Cutscene started.");  // Optional: for debugging
        }
    }

    public void StopTimeline()
    {
        if (director != null)
        {
            director.Stop();
            Debug.Log("Cutscene stopped.");  // Optional: for debugging
        }
    }
}
