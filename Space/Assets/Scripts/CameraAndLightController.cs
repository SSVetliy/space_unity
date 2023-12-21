using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAndLightController : MonoBehaviour
{
    [SerializeField] Light light;
    Vector3 planet;
    Vector3 targCamera;
    Vector3 tarfLight;
    bool isMoveCamera;
    bool isMoveLight;
    void Start()
    {
        isMoveCamera = false;
        isMoveLight = false;
        planet = Vector3.zero;
    }

    void Update()
    {
        if (isMoveCamera)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, targCamera, 10 * Time.deltaTime);
            if (Camera.main.transform.position == targCamera)
                isMoveCamera = false;
        }

        if (isMoveLight)
        {
            light.transform.position = Vector3.MoveTowards(light.transform.position, tarfLight, 10 * Time.deltaTime);
            if (light.transform.position == tarfLight)
                isMoveLight = false;
        }
    }

    private void OnEnable()
    {
        GameController.onConnectPlanet += Move;
    }

    private void OnDisable()
    {
        GameController.onConnectPlanet -= Move;
    }

    private void Move(int id)
    {
        planet = new Vector3(0, 0, 30 * id);
        isMoveCamera = true;
        targCamera = new Vector3(planet.x, planet.y + 89.6f, planet.z - 28);
        isMoveLight = true;
        tarfLight = new Vector3(planet.x - 11, planet.y + 6, planet.z - 6);
    }
}
