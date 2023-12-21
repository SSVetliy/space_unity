using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController music = null; 

    void Start()
    {
        if  (music != null && music != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            music = this;
        }
    }
}
