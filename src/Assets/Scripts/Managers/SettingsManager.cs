using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    // Prefs Names
    private const string _usernamePrefsName = "username";
    private const string _musicVolumePrefsName = "musicVolume";
    private const string _difficultyPrefsName = "difficulty";

    // Initialize instance when Awake is called
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Duplicate instance of SettingsManager found, destroying game object.");
            Destroy(gameObject);
        }
    }

    // Username Functions
    public string GetUsername()
    {
        string username = PlayerPrefs.GetString(_usernamePrefsName, "Lex Starwalker");
        
        if (username == "")
        {
            username = "Lex Starwalker";
        }

        return username;
    }

    public void UpdateUsername(string username)
    {
        PlayerPrefs.SetString(_usernamePrefsName, username);
    }

    // Music Volume Functions
    public float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(_musicVolumePrefsName, 0.5f);
    }

    public void UpdateMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat(_musicVolumePrefsName, volume);
    }

    // Difficulty Functions
    public int GetDifficulty()
    {
        return PlayerPrefs.GetInt(_difficultyPrefsName, 1);
    }

    public void UpdateDifficulty(int difficulty)
    {
        PlayerPrefs.SetInt(_difficultyPrefsName, difficulty);
    }
}
