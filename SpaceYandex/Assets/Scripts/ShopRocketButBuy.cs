using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameController;

public class ShopRocketButBuy : MonoBehaviour
{
    [SerializeField] ShopRocket[] arrShopRocket;
    ShopRocket rocket = null;

    [SerializeField] AudioSource noMoneySound;
    [SerializeField] AudioSource clickButSound;

    void Start()
    {
        var data = SaveLoadData.DATA;
        int id = data.currentRocket;
        for (int i = 0; i < data.buyItemsRocket.Length; i++)
        {
            if (data.buyItemsRocket[i]) arrShopRocket[i].GetLockImage.SetActive(false);
            if (i == id - 1) arrShopRocket[i].GetComponent<Image>().color = Color.green;
            else arrShopRocket[i].GetComponent<Image>().color = Color.white;
        }

        
    }
    
    void Update()
    {
        
    }

    private void OnEnable()
    {
        ShopRocket.onWantBuyRocket += SetRocket;
        ShopController.onOpenShop += Start;
    }

    private void OnDisable()
    {
        ShopRocket.onWantBuyRocket -= SetRocket;
        ShopController.onOpenShop -= Start;
    }

    private void SetRocket(ShopRocket rocket)
    {
        this.rocket = rocket;
    }

    public void BuyRocket()
    {
        if (rocket != null)
        {
            var data = SaveLoadData.DATA;
            if (data.money >= rocket.GetPriceItem && !data.buyItemsRocket[rocket.GetId - 1])
            {
                clickButSound.Play();
                data.money -= rocket.GetPriceItem;
                data.buyItemsRocket[rocket.GetId - 1] = true;
                data.currentRocket = rocket.GetId;
                
                for(int i = 0; i < arrShopRocket.Length; i++)
                {
                    if(i == rocket.GetId - 1) arrShopRocket[i].GetComponent<Image>().color = Color.green;
                    else arrShopRocket[i].GetComponent<Image>().color = Color.white;
                }
                

                //SaveLoadData.Save();

                rocket.isBuy = true;
                rocket.GetLockImage.SetActive(false);
                ShopController.onBuyItemShop?.Invoke();
            }
            else
                noMoneySound.Play();
        }
    }

    public void ChooseCurrentRocket(int id)
    {
        for (int i = 0; i < arrShopRocket.Length; i++)
        {
            if (i == id - 1) arrShopRocket[i].GetComponent<Image>().color = Color.green;
            else arrShopRocket[i].GetComponent<Image>().color = Color.white;
        }

        var data = SaveLoadData.DATA;
        data.currentRocket = id;
        //SaveLoadData.Save();
    }
}
