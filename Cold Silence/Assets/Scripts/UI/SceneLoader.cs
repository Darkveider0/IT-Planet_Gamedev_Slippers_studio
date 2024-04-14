using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //Это скрипт для меню
    public void New_Game(string sceneName)
    {
        //новая игра
        LoadScene(sceneName);
    }
    public void Continue(string sceneName)
    {
        LoadScene(sceneName);
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
