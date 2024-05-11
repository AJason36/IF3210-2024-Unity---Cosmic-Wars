using UnityEngine;
using UnityEngine.Playables;

public class TimelineSkip : MonoBehaviour
{
    public PlayableDirector timeline;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            timeline.time = timeline.duration;
        }
    }
}
