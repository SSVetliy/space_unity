using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static GameController;

public class CanvasController : MonoBehaviour
{
    [SerializeField] Canvas loseCanvas;
    [SerializeField] TextMeshProUGUI loseCanvasText;
    [SerializeField] Canvas startCanvas;
    [SerializeField] Canvas mainCanvas;

    float maxFuel;
    float fuel;
    [SerializeField] Image fuelPanel;
    [SerializeField] TextMeshProUGUI fuelText;

    [SerializeField] GameObject settingsCanvas;

    bool isPlayGame = false;

    void Start()
    {
        maxFuel = SaveLoadData.DATA.maxFuel;
        fuel = maxFuel;
    }

    void Update()
    {
        if (isPlayGame)
        {
            fuel -= 10 * Time.deltaTime;
            fuelPanel.fillAmount = fuel / maxFuel;
            fuelText.text = (int)fuel + "/" + maxFuel;
        }
        if(isPlayGame && fuel < 0)
        {
            GameController.onGameOver?.Invoke("Закончилось топтиво");
        }
    }

    void ConnectPlanet(int i)
    {
        fuel = Mathf.Clamp(fuel + 90, 0, maxFuel);
    }

    private void OnEnable()
    {
        GameController.onGameOver += GameOver;
        GameController.onPlayGame += PlayGame;
        GameController.onConnectPlanet += ConnectPlanet;
    }

    private void OnDisable()
    {
        GameController.onGameOver -= GameOver;
        GameController.onPlayGame -= PlayGame;
        GameController.onConnectPlanet -= ConnectPlanet;
    }

    void GameOver(string message)
    {
        isPlayGame = false;
        mainCanvas.enabled = false;
        loseCanvas.enabled = true;
        loseCanvasText.text = message;
    }

    public void ReloadLvl()
    {
        GameController.onGameOver?.Invoke("Ты сам так захотел");
    }

    void PlayGame()
    {
        isPlayGame = true;
        startCanvas.enabled = false;
        maxFuel = SaveLoadData.DATA.maxFuel;
        fuel = maxFuel;
    }

    public void OpenSettings()
    {
        settingsCanvas.SetActive(true);
        startCanvas.enabled = false;
    }

    public void CloseSettings()
    {
        settingsCanvas.SetActive(false);
        startCanvas.enabled = true;
        SaveLoadData.Save();
    }
}
