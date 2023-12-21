using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using TMPro;

public class Yandex : MonoBehaviour
{
    [DllImport("__Internal")]
    public static extern void SaveExtern(string data);
    [DllImport("__Internal")]
    public static extern void LoadExtern();

    [DllImport("__Internal")]
    public static extern void AddCoinsExtern(int count);

    [DllImport("__Internal")]
    public static extern void AddCoinsExtern2(int count);

    [DllImport("__Internal")]
    public static extern void ShowAdv();

    private void Start()
    {
        ShowAdbPage();
    }

    public static void ShowAdbPage()
    {
        MusicController.music.GetComponent<AudioSource>().Pause();
        ShowAdv();
    }

    public void PlayBackgroundMusic()
    {
        MusicController.music.GetComponent<AudioSource>().Play();
    }

    public void SetDATA(string data)
    {
        SaveLoadData.DATA = JsonUtility.FromJson<SaveData>(data);
    }

    public void MultOn2MoneyAdv(int score)
    {
        MusicController.music.GetComponent<AudioSource>().Pause();
        AddCoinsExtern(score);
    }

    public void AddMoneyStartMenu(int score)
    {
        MusicController.music.GetComponent<AudioSource>().Pause();
        AddCoinsExtern2(score);
    }

    public void AddMoneyAdv(int score)
    {
        var data = SaveLoadData.DATA;
        data.money += score;
        SaveLoadData.Save();
        MusicController.music.GetComponent<AudioSource>().Play();
        new SceneChanger().ChangeScene("level");
    }

    public void AddMoneyAdv2(int score)
    {
        var data = SaveLoadData.DATA;
        data.money += score;
        SaveLoadData.Save();
        MusicController.music.GetComponent<AudioSource>().Play();
        GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameController>().ChooseMoneyText();
    }

    private void OnEnable()
    {
        GameController.onShowAdv += MultOn2MoneyAdv;
        GameController.onShowAdv2 += AddMoneyStartMenu;
    }

    private void OnDisable()
    {
        GameController.onShowAdv -= MultOn2MoneyAdv;
        GameController.onShowAdv2 -= AddMoneyStartMenu;
    }
}
