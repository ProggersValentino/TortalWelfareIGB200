using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void doExitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

    public void doChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
