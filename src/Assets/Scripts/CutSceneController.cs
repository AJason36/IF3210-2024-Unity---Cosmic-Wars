using UnityEngine;
using UnityEngine.Playables;
using System.Collections;
using System.Collections.Generic;

namespace Nightmare
{
    public class CutSceneController : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject player;

    private bool cutsceneStarted = false;

    void Update()
    {
        if (Input.GetButtonDown("Skill") && !cutsceneStarted)
        {
            StartCutscene();
        }
    }

    void StartCutscene()
    {
        playableDirector.Play();
        // PauseGame();
        // cutsceneStarted = true;
    }

    void PauseGame()
    {
        Time.timeScale = 0; // Pauses the game time
        player.GetComponent<CharacterController>().enabled = false; // Disable player control
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; // Resume game time
        player.GetComponent<CharacterController>().enabled = true; // Enable player control
    }
}
}