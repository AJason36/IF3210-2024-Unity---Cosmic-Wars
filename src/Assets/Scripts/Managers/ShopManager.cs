using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    // Shop Canvas
    public Canvas shopCanvas;

    // Popup Objects
    public Canvas shopPopup;
    public Image darkOverlay;

    // ItemID
    // 0 for R2D2
    // 1 for BB8
    private int itemID;

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
        CanvasGroup canvasGroup = shopCanvas.GetComponent<CanvasGroup>();

        if (canvasGroup != null)
        {
            canvasGroup.interactable = interactable;
            canvasGroup.blocksRaycasts = interactable;
        }
    }

    public void OnRDBuyButtonClicked()
    {
        OpenShopPopup();
        itemID = 0;
    }

    public void OnBBBuyButtonClicked()
    {
        OpenShopPopup();
        itemID = 1;
    }

    public void OnBuyButtonClicked()
    {
        // Buy the item
        if (itemID == 0)
        {
            // TODO: Buy R2D2
            Debug.Log("R2D2 bought");
        }
        else if (itemID == 1)
        {
            // TODO: Buy BB8
            Debug.Log("BB8 bought");
        }

        CloseShopPopup();
        itemID = -1;
    }

    public void OnCancelButtonClicked()
    {
        CloseShopPopup();
        itemID = -1;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Just in case
        shopPopup.gameObject.SetActive(false);
        darkOverlay.gameObject.SetActive(false);

        itemID = -1;
    }
}
