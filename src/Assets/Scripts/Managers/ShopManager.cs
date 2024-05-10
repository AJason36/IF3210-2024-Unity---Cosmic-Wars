using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    // Shop Canvas
    public Canvas shopCanvas;

    // Popup Objects
    public Canvas shopPopup;
    public Image darkOverlay;

    // Price Text
    public TMP_Text rdPriceText;
    public TMP_Text bbPriceText;

    // Buy Buttons
    public Button rdBuyButton;
    public Button bbBuyButton;

    // Item Price
    private int rdPrice = 100;
    private int bbPrice = 50;

    // ItemID
    // 0 for R2D2
    // 1 for BB8
    private int itemID;

    public void SetShopActive(){
      shopCanvas.gameObject.SetActive(true);
      Cursor.lockState = CursorLockMode.None;
      // SetCanvasInteractable(true);

        rdPriceText.text = "$" + rdPrice.ToString();
        bbPriceText.text = "$" + bbPrice.ToString();

        // Set interactivity of the buy buttons
        if (DataPersistenceManager.Instance.GetGameData().money < rdPrice)
        {
            rdBuyButton.interactable = false;
        }
        else
        {
            rdBuyButton.interactable = true;
        }

        if (DataPersistenceManager.Instance.GetGameData().money < bbPrice)
        {
            bbBuyButton.interactable = false;
        }
        else
        {
            bbBuyButton.interactable = true;
        }
    }

    public void SetShopInactive(){
      shopCanvas.gameObject.SetActive(false);
      Cursor.lockState = CursorLockMode.Locked;
    }

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
            DataPersistenceManager.Instance.GetGameData().money -= rdPrice;
            DataPersistenceManager.Instance.GetGameData().petId = 0;
            Debug.Log("R2D2 bought");
        }
        else if (itemID == 1)
        {
            DataPersistenceManager.Instance.GetGameData().money -= bbPrice;
            DataPersistenceManager.Instance.GetGameData().petId = 1;
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
