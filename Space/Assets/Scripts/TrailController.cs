using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    TrailRenderer trail;
    
    void Start()
    {
        trail = GetComponent<TrailRenderer>();
    }

    private void OnEnable()
    {
        GameController.onMouseClickDown += MouseClickDown;
        GameController.onMouseClickUp += MouseClickUp;
    }

    private void OnDisable()
    {
        GameController.onMouseClickDown -= MouseClickDown;
        GameController.onMouseClickUp -= MouseClickUp;
    }

    private void MouseClickDown()
    {
        trail.time = 0;
    }

    private void MouseClickUp()
    {
        trail.time = 2f;
    }
}
