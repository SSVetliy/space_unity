using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    void Update()
    {
        if(SaveLoadData.DATA.isLoad == 1)
        {
            new SceneChanger().ChangeScene("level");
        }
    }
}
