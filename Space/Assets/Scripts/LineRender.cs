using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRender : MonoBehaviour
{
    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    public void Show(Vector3 player, Vector3 origin, float speed)
    {
        line.positionCount = 2;
        line.SetPosition(0, origin);
        line.SetPosition(1, origin + (origin - player).normalized * speed * 2);
    }

    public void Clear()
    {
        line.positionCount = 0;
    }
}
