using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using static GameController;

public class ShopController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI moneyTextInShop;

    [SerializeField] Canvas shopCanvas;
    [SerializeField] TextMeshProUGUI springForcePriceText;

    [SerializeField] TextMeshProUGUI fuelPriceText;
    [SerializeField] GameObject fuelAndSpringHint;
    [SerializeField] TextMeshProUGUI fuelAndSpringHintText;

    public static float springForse;

    [SerializeField] Image barSpring;
    [SerializeField] Image barSpringBackground;
    [SerializeField] GameObject buySpringBut;

    [SerializeField] Image barFuel;
    [SerializeField] Image barFuelBackground;
    [SerializeField] GameObject buyFuelBut;

    public static Action onOpenShop;
    public static Action onCloseShop;
    public static Action onBuyItemShop;

    [SerializeField] GameObject springCanvas;
    [SerializeField] GameObject rocketCanvas;

    [SerializeField] Canvas startCanvar;

    [SerializeField] AudioSource noMoneySound;
    [SerializeField] AudioSource clickButSound;

    void Start()
    {
        var data = SaveLoadData.DATA;
        springForse = data.springForce;
        moneyTextInShop.text = data.money.ToString();
        barSpring.fillAmount = data.springForce / 15f;

        barFuel.fillAmount = data.maxFuel / 1000f;

        if (springForse >= 15)
        {
            springForcePriceText.text = "";
            buySpringBut.SetActive(false);
            barSpringBackground.rectTransform.anchorMax = new Vector2(0.8608752f, 0.6874815f);
            barSpring.rectTransform.anchorMax = new Vector2(0.8608752f, 0.6874815f);
        }
        else
        {
            springForcePriceText.text = (CalcPriceSpringAndfuel(springForse + 0.2f)).ToString();
        }

        if(data.maxFuel >= 1000)
        {
            fuelPriceText.text = "";
            buyFuelBut.SetActive(false);
            barFuelBackground.rectTransform.anchorMax = new Vector2(0.8608752f, 0.5265927f);
            barFuel.rectTransform.anchorMax = new Vector2(0.8608752f, 0.5265927f);
        }
        else
        {
            fuelPriceText.text = (CalcPriceSpringAndfuel((data.maxFuel + 20) / 100f)).ToString();
        }
    }

   
    void Update()
    {

    }

    private void OnEnable()
    {
        onBuyItemShop += BuyItemShop;
    }

    private void OnDisable()
    {
        onBuyItemShop -= BuyItemShop;
    }

    void BuyItemShop()
    {
        var data = SaveLoadData.DATA;
        moneyTextInShop.text = data.money.ToString();
    }

    public void OpenShop()
    {
        shopCanvas.enabled = true;
        startCanvar.enabled = false;
        onOpenShop?.Invoke();
    }

    public void CloseShop()
    {
        shopCanvas.enabled = false;
        startCanvar.enabled = true;
        SaveLoadData.Save();
        onCloseShop?.Invoke();
    }

    public void OpenSpringShop()
    {
        springCanvas.SetActive(true);
        rocketCanvas.SetActive(false);
    }

    public void OpenRocketShop()
    {
        springCanvas.SetActive(false);
        rocketCanvas.SetActive(true);
    }

    int CalcPriceSpringAndfuel(float spring)
    {
        return (int)((spring + 50) * (spring + 50) * 0.02f);
    }

    public void AddSpringForce()
    {
        if (springForse < 15)
        {
            var data = SaveLoadData.DATA;
            int springForsePrice = CalcPriceSpringAndfuel(springForse + 0.2f);
            if (data.money >= springForsePrice)
            {
                clickButSound.Play();

                springForse = (float)Math.Round(springForse + 0.2f, 1);
                springForcePriceText.text = (CalcPriceSpringAndfuel(springForse + 0.2f)).ToString();

                data.springForce = springForse;
                data.money -= springForsePrice;

                moneyText.text = data.money.ToString();
                moneyTextInShop.text = data.money.ToString();

                barSpring.fillAmount = springForse / 15f;

                //Save("save", data);
            }
            else
                noMoneySound.Play();
        }
        if (springForse >= 15)
        {
            springForcePriceText.text = "";
            buySpringBut.SetActive(false);
            barSpringBackground.rectTransform.anchorMax = new Vector2(0.8608752f, 0.6874815f);
            barSpring.rectTransform.anchorMax = new Vector2(0.8608752f, 0.6874815f);
        }
    }

    public void AddFuel()
    {
        var data = SaveLoadData.DATA;
        int fuel = data.maxFuel;
        if(data.maxFuel < 1000)
        {
            int fuelPrice = CalcPriceSpringAndfuel((data.maxFuel + 20) / 100f);
            if (data.money >= fuelPrice)
            {
                clickButSound.Play();

                fuelPriceText.text = (CalcPriceSpringAndfuel((data.maxFuel + 40) / 100f)).ToString();

                data.maxFuel += 20;
                fuel += 20;
                data.money -= fuelPrice;

                moneyText.text = data.money.ToString();
                moneyTextInShop.text = data.money.ToString();

                barFuel.fillAmount = data.maxFuel / 1000f;

                //Save("save", data);
            }
            else
                noMoneySound.Play();
        }
        if (fuel >= 1000)
        {
            fuelPriceText.text = "";
            buyFuelBut.SetActive(false);
            barFuelBackground.rectTransform.anchorMax = new Vector2(0.8608752f, 0.5265927f);
            barFuel.rectTransform.anchorMax = new Vector2(0.8608752f, 0.5265927f);
        }
    }


    public void ShowHintSpringForce()
    {
        StartCoroutine(ShowTextHintSpringAndFuel(1));
    }

    public void ShowHintFuel()
    {
        StartCoroutine(ShowTextHintSpringAndFuel(2));
    }

    public void CloseHintSpringForceAndFuel()
    {
        fuelAndSpringHint.SetActive(false);
    }

    IEnumerator ShowTextHintSpringAndFuel(int i)
    {
        fuelAndSpringHint.SetActive(true);
        if (i == 1)
            fuelAndSpringHintText.text = "Сила пружинки, которая стабилизирует орбиту, не дает ракете столкнуться с планетой и улететь от нее. При больших значения выглядит нереалистично)";
        else if (i == 2)
            fuelAndSpringHintText.text = "Увеличивает объем топливного бака, тем самым увеличивает время полета ракеты";
        
        yield return new WaitForSeconds(5f);
        fuelAndSpringHint.SetActive(false);
    }
}

