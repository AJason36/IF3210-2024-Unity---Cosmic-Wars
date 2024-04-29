using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UsernameManager : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("username"))
        {
            PlayerPrefs.SetString("username", "Lex Starwalker");
        }

        Load();
    }

    public void changeText()
    {
        Save();
    }

    private void Load()
    {
        inputField.text = PlayerPrefs.GetString("username");
    }

    private void Save()
    {
        PlayerPrefs.SetString("username", inputField.text);
    }
}
