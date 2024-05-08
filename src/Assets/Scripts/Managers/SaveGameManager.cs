using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveGameManager : MonoBehaviour
{
    // Canvas
    public Canvas menuCanvas;

    // Popup Objects
    public Canvas shopPopup;
    public Image darkOverlay;

    // Save ID
    private int saveId;

    public void OpenShopPopup()
    {
        shopPopup.gameObject.SetActive(true);
        darkOverlay.gameObject.SetActive(true);
        SetCanvasInteractable(false);
    }

    public void CloseShopPopup()
    {
        shopPopup.gameObject.SetActive(false);
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

    // Start is called before the first frame update
    void Start()
    {
        // Just in case
        shopPopup.gameObject.SetActive(false);
        darkOverlay.gameObject.SetActive(false);

        saveId = -1;
    }
}
