using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using static GameController;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    bool isMousePress;
    bool isActite;
    Vector3 shoot;

    int curPlanetId;

    [SerializeField] GameObject model;
    [SerializeField] ParticleSystem particle;
    [SerializeField] LineRender line;

    [SerializeField] AudioSource explosionRocketSound;

    public Vector2 mousePosition;
    public Vector3 savePlayerPosition;

    [SerializeField] Texture2D[] skinsRocket;

    public int CurPlanetId
    {
        get
        {
            return curPlanetId;
        }
        set
        {
            curPlanetId = value;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * 60);
        isMousePress = false;
        isActite = true;

        SwitchModelRocket();
    }

    void Update()
    {
        if (isActite)
        {
            if (isMousePress)
            {
                rb.position = savePlayerPosition + new Vector3(mousePosition.x, 0, mousePosition.y);
                if ((shoot - transform.position) != Vector3.zero)
                    model.transform.forward = Vector3.MoveTowards(transform.up, (shoot - transform.position).normalized, 100);
            }
            else if(rb.velocity != Vector3.zero)
            {
                model.transform.forward = Vector3.MoveTowards(transform.up, rb.velocity.normalized, 100);
            }
        }
    }

    void FixedUpdate()
    {
        if (isActite)
        {
            if (isMousePress)
            {
                line.Show(transform.position, shoot, (transform.position - shoot).magnitude);
            }
        }
    }

    void SwitchModelRocket()
    {
        var data = SaveLoadData.DATA;
        int curRocket = data.currentRocket - 1;

        model.GetComponentInChildren<MeshRenderer>().material.SetTexture("_MainTex", skinsRocket[curRocket]);

    }

    private void OnEnable()
    {
        GameController.onMouseClickDown += MouseClickDown;
        GameController.onMouseClickUp += MouseClickUp;
        GameController.onConnectPlanet += ConnectPlanet;
        GameController.onGameOver += GameOver;

        ShopController.onCloseShop += SwitchModelRocket;
        
    }

    private void OnDisable()
    {
        GameController.onMouseClickDown -= MouseClickDown;
        GameController.onMouseClickUp -= MouseClickUp;
        GameController.onConnectPlanet -= ConnectPlanet;
        GameController.onGameOver -= GameOver;

        ShopController.onCloseShop -= SwitchModelRocket;
    }

    private void MouseClickDown()
    {
        isMousePress = true;
        rb.isKinematic = true;
        shoot = transform.position;
        savePlayerPosition = rb.position;
    }

    private void MouseClickUp()
    {
        line.Clear();
        rb.isKinematic = false;
        rb.AddForce((shoot - transform.position) * 15);
        isMousePress = false;
        isActite = false;
        GameController.onDisConnectPlanet?.Invoke();
    }

    private void ConnectPlanet(int id)
    {
        isActite = true;
    }

    public void GameOver(string message)
    {
        explosionRocketSound.Play();
        rb.velocity = Vector3.zero;
        isActite = false;
        Destroy(model.gameObject);
        particle.Play(true);
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeInHierarchy)
        {
            GameController.onGameOver?.Invoke("Пролетел");
        }
    }
}
