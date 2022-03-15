using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAnimalia : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(sceneName: "Level1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void GameMenu()
    {
        SceneManager.LoadScene(sceneName: "Menu");
    }
}
