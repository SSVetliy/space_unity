using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName) 
    {
        SceneManager.LoadScene(sceneName); 
    }

    public void ChangeSceneBut()
    {
        MusicController.music.GetComponent<AudioSource>().Pause();
        Yandex.ShowAdbPage();
       
        SceneManager.LoadScene("level");
    }
    public void Exit() 
    { 
        Application.Quit(); 
    }
}
