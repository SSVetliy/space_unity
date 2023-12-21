using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static Action onMouseClickDown;
    public static Action onMouseClickUp;
    public static Action<int> onMouseClickDownToPlanet;
    public static Action<int> onMouseClickUpToPlanet;
    public static Action onPlayGame;
    public static Action<string> onGameOver;
    public static Action<int> onConnectPlanet;
    public static Action<int> onShowAdv;
    public static Action<int> onShowAdv2;
    public static Action onDisConnectPlanet;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Canvas mainCanvas;
    [SerializeField] TextMeshProUGUI recordText;
    [SerializeField] TextMeshProUGUI moneyText;

    public int currentRocketPlayer;

    public static float springForse;
    int score;

    [SerializeField] TextMeshProUGUI scoreTextGameOver;
    [SerializeField] TextMeshProUGUI maxScoreTextGameOver;

    [SerializeField] AudioSource soundNoMoney;
    [SerializeField] AudioSource soundBuy;
    [SerializeField] AudioSource soundExplosion;

    [SerializeField] GameObject addButStartMenu;


    void Start()
    {
        scoreText.color = new Color(0, 0, 0, 0);
        score = -1;

        var data = SaveLoadData.DATA;
        recordText.text = data.record.ToString();
        moneyText.text = data.money.ToString();

        MusicController.music.GetComponent<AudioSource>().volume = data.musicVolume;
        soundBuy.volume = data.soundVolume;
        soundNoMoney.volume = data.soundVolume;
        soundExplosion.volume = data.soundVolume;

    }

    void Update()
    {

    }

    // Для тестов
    public void ClearSave()
    {
        PlayerPrefs.DeleteAll();
        new SceneChanger().ChangeScene("level");
    }

    public void Add9999Money()
    {
        var data = SaveLoadData.DATA;
        data.money += 9999;
        SaveLoadData.Save();
        new SceneChanger().ChangeScene("level");
    }
    /// Для тестов


    private void OnEnable()
    {
        onGameOver += GameOver;
        onConnectPlanet += Connect;
        ShopController.onBuyItemShop += Start;
    }

    private void OnDisable()
    {
        onGameOver -= GameOver;
        onConnectPlanet -= Connect;
        ShopController.onBuyItemShop -= Start;
    }

    void Connect(int id)
    {
        score++;
        scoreText.text = score.ToString();
    }

    void GameOver(string message)
    {
        var data = SaveLoadData.DATA;
        data.record = score > data.record ? score : data.record;
        data.money = data.money + score;
        SaveLoadData.Save();
        scoreTextGameOver.text = "Заработанно монет: " + score.ToString();

        int maxScore = SaveLoadData.DATA.record;
        maxScoreTextGameOver.text = "Рекорд: " + (score > maxScore ? score : maxScore).ToString();
    }

    public void Play()
    {
        onPlayGame?.Invoke();
        mainCanvas.enabled = true;
        scoreText.text = "0";
        scoreText.color = new Color(255, 255, 255, 1);
    }

    public void WatchAddLoseMenu()
    {
        onShowAdv?.Invoke(score);
    }

    public void WatchAddStartMenu()
    {
        onShowAdv2?.Invoke(50);
        addButStartMenu.SetActive(false);
    }

    public void ChooseMoneyText()
    {
        var data = SaveLoadData.DATA;
        moneyText.text = data.money.ToString();
    }


}
