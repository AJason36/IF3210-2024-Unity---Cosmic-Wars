using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveGameManager : MonoBehaviour
{
    // Data Persistence Manager
    private DataPersistenceManager dataPersistenceManager;

    // Canvas
    public Canvas menuCanvas;

    // Popup Objects
    public Canvas savePopup;
    public Image darkOverlay;

    // Save Buttons
    public Button saveOneButton;
    public Button saveTwoButton;
    public Button saveThreeButton;

    // Save ID
    private int saveId;

    public void SetSaveOpen(){
      menuCanvas.gameObject.SetActive(true);
      Cursor.lockState = CursorLockMode.None;
    }

    public void SetSaveClose(){
      menuCanvas.gameObject.SetActive(false);
      Cursor.lockState = CursorLockMode.Locked;
    }

    public void OpenSavePopup()
    {
        savePopup.gameObject.SetActive(true);
        darkOverlay.gameObject.SetActive(true);
        SetCanvasInteractable(false);
    }

    public void CloseSavePopup()
    {
        savePopup.gameObject.SetActive(false);
        darkOverlay.gameObject.SetActive(false);
        SetCanvasInteractable(true);
    }

    private void SetCanvasInteractable(bool interactable)
    {
        CanvasGroup canvasGroup = menuCanvas.GetComponent<CanvasGroup>();

        if (canvasGroup != null)
        {
            canvasGroup.interactable = interactable;
            canvasGroup.blocksRaycasts = interactable;
        }
    }

    public void SaveGame()
    {
        if (saveId == -1)
        {
            Debug.LogError("Save ID not set.");
            return;
        }

        dataPersistenceManager.SaveGame(saveId.ToString());
        CloseSavePopup();
    }

    public void SaveOne()
    {
        dataPersistenceManager.GetGameData().saveName = "Save 1";
        saveId = 1;
        OpenSavePopup();
    }

    public void SaveTwo()
    {
        dataPersistenceManager.GetGameData().saveName = "Save 2";
        saveId = 2;
        OpenSavePopup();
    }

    public void SaveThree()
    {
        dataPersistenceManager.GetGameData().saveName = "Save 3";
        saveId = 3;
        OpenSavePopup();
    }

    public void CancelSave()
    {
        CloseSavePopup();
    }

    private void LoadSaveDataMenu()
    {
        // Get All Save Data
        Dictionary<string, GameData> saves = dataPersistenceManager.GetAllSaves();

        // Load Save Data
        if (saves.ContainsKey("1")) {
            LoadSaveDataButton(saves["1"], saveOneButton);
        } else {
            LoadSaveDataButton(null, saveOneButton);
        }

        if (saves.ContainsKey("2")) {
            LoadSaveDataButton(saves["2"], saveTwoButton);
        } else {
            LoadSaveDataButton(null, saveTwoButton);
        }

        if (saves.ContainsKey("3")) {
            LoadSaveDataButton(saves["3"], saveThreeButton);
        } else {
            LoadSaveDataButton(null, saveThreeButton);
        }
    }

    private void LoadSaveDataButton(GameData data, Button button)
    {
        if (data == null)
        {
            for (int i = 0; i < button.transform.childCount; i++)
            {
                Transform child = button.transform.GetChild(i);
                if (child.name == "EmptyText")
                {
                    child.gameObject.SetActive(true);
                } else {
                    child.gameObject.SetActive(false);
                }
            }
        } else {
            for (int i = 0; i < button.transform.childCount; i++)
            {
                Transform child = button.transform.GetChild(i);

                if (child.name == "EmptyText")
                {
                    child.gameObject.SetActive(false);
                    continue;
                } else {
                    child.gameObject.SetActive(true);
                }

                if (child.name == "SaveNameText") {
                    child.GetComponent<TMP_Text>().text = data.saveName;
                } else if (child.name == "LastUpdatedText") {
                    System.DateTime dateTime = System.DateTime.FromBinary(data.lastUpdated);
                    child.GetComponent<TMP_Text>().text = dateTime.ToString("dd/MM/yyyy");
                } else {
                    child.GetComponent<TMP_Text>().text = "Level " + data.level;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dataPersistenceManager = DataPersistenceManager.Instance;

        // TODO: Remove this after integrated
        dataPersistenceManager.NewGame();
        
        LoadSaveDataMenu();

        // Just in case
        savePopup.gameObject.SetActive(false);
        darkOverlay.gameObject.SetActive(false);

        saveId = -1;
    }
}
