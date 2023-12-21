using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[SerializeField]
public class SaveData
{
    public int isLoad;
    public int record;
    public int money;
    public float springForce;
    public bool[] buyItemsRocket;
    public int currentRocket;
    public int maxFuel;
    public float musicVolume;
    public float soundVolume;

    public SaveData()
    {
        record = 0;
        money = 0;
        springForce = 0;
        currentRocket = 1;
        buyItemsRocket = new bool[6];
        buyItemsRocket[0] = true;
        musicVolume = 1f;
        soundVolume = 1f;
        isLoad = 1;
        for (int i = 1; i < 6; i++)
        {
            buyItemsRocket[i] = false;
        }
        maxFuel = 100;
    }
}

public class SaveLoadData : MonoBehaviour
{
    public static SaveData DATA;

    void Start()
    {
        Load();
    }

    public static void Save()
    {
        string json = JsonUtility.ToJson(DATA, true);
        Yandex.SaveExtern(json);
    }

    public static void Load()
    {
        Yandex.LoadExtern();
    }   
}
