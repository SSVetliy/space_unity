using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{

    [SerializeField] GameObject canvas;
    [SerializeField] Canvas shopCanvas;
    [SerializeField] Canvas startCanvas;
    [SerializeField] Button reloadLvl;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnEnable()
    {
        GameController.onGameOver += GameOver;
        GameController.onPlayGame += PlayGame;
    }

    private void OnDisable()
    {
        GameController.onGameOver -= GameOver;
        GameController.onPlayGame -= PlayGame;
    }

    void GameOver(string message)
    {
        if (!canvas.activeSelf)
        {
            canvas.SetActive(true);
            canvas.GetComponentsInChildren<TextMeshProUGUI>()[0].text = message;
        }
    }

    public void OpenShop()
    {
        shopCanvas.enabled = true;
    }

    public void CloseShop()
    {
        shopCanvas.enabled = false;
    }

    public void ReloadLvl()
    {
        GameController.onGameOver?.Invoke("Ты сам так захотел");
    }

    void PlayGame()
    {
        startCanvas.enabled = false;
        reloadLvl.gameObject.SetActive(true);
    }
}
