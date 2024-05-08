using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameManager : MonoBehaviour
{
    // Data Persistence Manager
    private DataPersistenceManager dataPersistenceManager;

    // Canvas
    public Canvas menuCanvas;

    // Popup Objects
    public Canvas loadPopup;
    public Image darkOverlay;

    // Save Buttons
    public Button loadOneButton;
    public Button loadTwoButton;
    public Button loadThreeButton;

    // Save ID
    private int saveId;

    public void OpenLoadPopup()
    {
        loadPopup.gameObject.SetActive(true);
        darkOverlay.gameObject.SetActive(true);
        SetCanvasInteractable(false);
    }

    public void CloseSavePopup()
    {
        loadPopup.gameObject.SetActive(false);
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

    // TODO: Implement Load Save Data

    public void CancelSave()
    {
        CloseSavePopup();
    }

    private void LoadSaveDataMenu()
    {
        // Get All Save Data
        Dictionary<string, GameData> saves = dataPersistenceManager.GetAllSaves();

        // Load Save Data
        if (saves.ContainsKey("1"))
        {
            LoadLoadDataButton(saves["1"], loadOneButton);
        }
        else
        {
            LoadLoadDataButton(null, loadOneButton);
        }

        if (saves.ContainsKey("2"))
        {
            LoadLoadDataButton(saves["2"], loadTwoButton);
        }
        else
        {
            LoadLoadDataButton(null, loadTwoButton);
        }

        if (saves.ContainsKey("3"))
        {
            LoadLoadDataButton(saves["3"], loadThreeButton);
        }
        else
        {
            LoadLoadDataButton(null, loadThreeButton);
        }
    }

    private void LoadLoadDataButton(GameData data, Button button)
    {
        if (data == null)
        {
            for (int i = 0; i < button.transform.childCount; i++)
            {
                Transform child = button.transform.GetChild(i);
                if (child.name == "EmptyText")
                {
                    child.gameObject.SetActive(true);
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < button.transform.childCount; i++)
            {
                Transform child = button.transform.GetChild(i);

                if (child.name == "EmptyText")
                {
                    child.gameObject.SetActive(false);
                    continue;
                }
                else
                {
                    child.gameObject.SetActive(true);
                }

                if (child.name == "SaveNameText")
                {
                    child.GetComponent<TMP_Text>().text = data.saveName;
                }
                else if (child.name == "LastUpdatedText")
                {
                    System.DateTime dateTime = System.DateTime.FromBinary(data.lastUpdated);
                    child.GetComponent<TMP_Text>().text = dateTime.ToString("dd/MM/yyyy");
                }
                else
                {
                    child.GetComponent<TMP_Text>().text = "Level " + data.level;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dataPersistenceManager = DataPersistenceManager.Instance;

        LoadSaveDataMenu();

        // Just in case
        loadPopup.gameObject.SetActive(false);
        darkOverlay.gameObject.SetActive(false);

        saveId = -1;
    }
}
