using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAndLightController : MonoBehaviour
{
    [SerializeField] Light light;
    Vector3 planet;
    Vector3 targCamera;
    Vector3 tarfLight;
    void Start()
    {
        planet = Vector3.zero;
    }

    void Update()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targCamera, 5 * Time.deltaTime);
        light.transform.position = Vector3.Lerp(light.transform.position, tarfLight, 5 * Time.deltaTime);
    }

    private void OnEnable()
    {
        GameController.onConnectPlanet += StartMove;
    }

    private void OnDisable()
    {
        GameController.onConnectPlanet -= StartMove;
    }

    private void StartMove(int id)
    {
        planet = new Vector3(0, 0, 30 * id);
        targCamera = new Vector3(planet.x, planet.y + 89.6f, planet.z - 28);
        tarfLight = new Vector3(planet.x - 11, planet.y + 6, planet.z - 6);
    }
}
