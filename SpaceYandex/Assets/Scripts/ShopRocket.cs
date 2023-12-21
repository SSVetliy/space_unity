using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameController;

public class ShopRocket : MonoBehaviour
{
    [SerializeField] int priceItem;
    [SerializeField] int id;
    [SerializeField] public bool isBuy { get; set; }
    [SerializeField] public GameObject lockImage;
    [SerializeField] TextMeshProUGUI priceItemText;
    [SerializeField] Sprite previewSprite;
    [SerializeField] Image preview;

    [SerializeField] GameObject butBuy;

    public static Action<ShopRocket> onWantBuyRocket;
    [SerializeField] ShopRocketButBuy shopRocketButBuy;

    public int GetId
    {
        get { return id; }
    }

    public int GetPriceItem
    {
        get { return priceItem; }
    }

    public GameObject GetLockImage
    {
        get { return lockImage; }
    }

    private void OnEnable()
    {
        ShopController.onOpenShop += InIt;
    }

    private void OnDisable()
    {
        ShopController.onOpenShop -= InIt;
    }

    private void Start()
    {
        InIt();
    }

    void InIt()
    {
        if (id > 0)
        {
            priceItemText.text = priceItem.ToString();
            var data = SaveLoadData.DATA;
            isBuy = data.buyItemsRocket[id - 1];
            if (isBuy)
            {
                lockImage.SetActive(false);
            }
            if (id == data.currentRocket)
            {
                shopRocketButBuy.ChooseCurrentRocket(id);
                preview.sprite = previewSprite;
            }
        }
    }

    public void BuyRocket()
    {
        preview.sprite = previewSprite;
        if (!isBuy)
        {
            butBuy.SetActive(true);
            onWantBuyRocket?.Invoke(this);
        }
        else
        {
            butBuy.SetActive(false);
            shopRocketButBuy.ChooseCurrentRocket(id);
        }
       
    }
}
