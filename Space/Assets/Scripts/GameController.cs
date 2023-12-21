using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static Action onMouseClickDown;
    public static Action onMouseClickUp;
    public static Action<int> onMouseClickDownToPlanet;
    public static Action<int> onMouseClickUpToPlanet;
    public static Action onPlayGame;
    public static Action<string> onGameOver;
    public static Action<int> onConnectPlanet;
    public static Action onDisConnectPlanet;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI recordText;
    [SerializeField] TextMeshProUGUI moneyText;

    [SerializeField] TextMeshProUGUI springForceText;
    [SerializeField] TextMeshProUGUI springForcePriceText;
    [SerializeField] TextMeshProUGUI springForceHint;

    public static float springForse;
    int score;

    void Start()
    {
        scoreText.color = new Color(0, 0, 0, 0);
        score = -1;
        var data = Load<SaveData>("save");
        recordText.text = data.maxMoney.ToString();
        moneyText.text = data.money.ToString();
        springForse = data.springForce;

        if (springForse >= 15)
        {
            springForceText.text = "Сила пружинки: full save";
            springForcePriceText.text = "";
        }
        else
        {
            springForceText.text = "Сила пружинки: " + springForse;
            springForcePriceText.text = (((float)Math.Round(springForse + 0.2f, 1) * 10)).ToString();
        }
    }

    /// Для тестов
    public void ClearSave()
    {
        PlayerPrefs.DeleteAll();
        new SceneChanger().ChangeScene("level");
    }

    public void Add9999Money()
    {
        var data = Load<SaveData>("save");
        data.money += 9999;
        Save<SaveData>("save", data);
        new SceneChanger().ChangeScene("level");
    }
    /// Для тестов


    private void OnEnable()
    {
        onGameOver += GameOver;
        onConnectPlanet += Connect;

    }

    private void OnDisable()
    {
        onGameOver -= GameOver;
        onConnectPlanet -= Connect;
    }

    void Connect(int id)
    {
        score++;
        scoreText.text = score.ToString();
    }

    void GameOver(string message)
    {
        Save();
    }

    public void Play()
    {
        onPlayGame?.Invoke();
        scoreText.text = "0";
        scoreText.color = new Color(255, 255, 255, 1);
    }

    public void ShowHintSpringForce()
    {
        StartCoroutine(ShowTextHint());
    }

    IEnumerator ShowTextHint()
    {
        springForceHint.enabled = true;
        yield return new WaitForSeconds(5f);
        springForceHint.enabled = false;
    }

    public void AddSpringForce()
    {
        if (springForse < 15)
        {
            var data = Load<SaveData>("save");
            int springForsePrice = (int)((springForse + 0.2f) * 10);
            if (data.money >= springForsePrice)
            {
                springForse = (float)Math.Round(springForse + 0.2f, 1);
                springForceText.text = "Сила пружинки: " + springForse;
                springForcePriceText.text = (((float)Math.Round(springForse + 0.2f, 1) * 10)).ToString();

                data.springForce = springForse;
                data.money -= springForsePrice;

                moneyText.text = data.money.ToString();

                Save("save", data);
            }
        }
        if (springForse >= 15)
        {
            springForceText.text = "Сила пружинки: full save";
            springForcePriceText.text = "";
        }
    }

    public static void Save<T>(string key, T data)
    {
        string json = JsonUtility.ToJson(data, true);
        PlayerPrefs.SetString(key, json);
    }

    public static T Load<T>(string key) where T : new()
    {
        if (PlayerPrefs.HasKey(key))
        {
            string load = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<T>(load);
        }
        return new T();
    }

    void Save()
    {
        var savedData = GameController.Load<SaveData>("save");

        var curData = new SaveData()
        {
            maxMoney = score > savedData.maxMoney ? score : savedData.maxMoney,
            money = savedData.money + score,
            springForce = springForse
        };

        GameController.Save<SaveData>("save", curData);
    }

    [Serializable] class SaveData
    {
        public int maxMoney;
        public int money;
        public float springForce;
    }
}
