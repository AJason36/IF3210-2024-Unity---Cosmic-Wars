using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    // Settings Manager Instance
    private SettingsManager settingsManager;

    // UI Elements
    [SerializeField] TMP_InputField usernameInputField;
    [SerializeField] Slider volumeSlider;
    [SerializeField] TMP_Dropdown difficultyDropdown;

    // Start is called before the first frame update
    void Start()
    {
        settingsManager = SettingsManager.Instance;
        usernameInputField.text = settingsManager.GetUsername();
        volumeSlider.value = settingsManager.GetMusicVolume();
        difficultyDropdown.value = settingsManager.GetDifficulty();
    }

    // When setActive is called and the value is true, refresh the values
    private void OnEnable()
    {
        settingsManager = SettingsManager.Instance;
        usernameInputField.text = settingsManager.GetUsername();
        volumeSlider.value = settingsManager.GetMusicVolume();
        difficultyDropdown.value = settingsManager.GetDifficulty();
    }

    public void UpdateUsername()
    {
        settingsManager.UpdateUsername(usernameInputField.text);
    }

    public void UpdateMusicVolume()
    {
        AudioListener.volume = volumeSlider.value;
        settingsManager.UpdateMusicVolume(volumeSlider.value);
    }

    public void UpdateDifficulty()
    {
        settingsManager.UpdateDifficulty(difficultyDropdown.value);
    }
}
