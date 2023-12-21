using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontReLoad : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        //Application.targetFrameRate = 30;
    }
}
