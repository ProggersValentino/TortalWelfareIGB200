using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausemenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    // main scene turn on/off. hopefully won't break anything
    public GameObject gameMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (GameIsPaused){
                Resume();
            } else
            {
                Pause();
            }
        }
    }
    public void Resume (){
        pauseMenuUI.SetActive(false);
        if (gameMenuUI != null) { gameMenuUI.SetActive(true); } // main scene turn on
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        if (gameMenuUI != null) { gameMenuUI.SetActive(false); } // main scene turn off
        Time.timeScale = 0f;
        GameIsPaused = true;

    }

    public void LoadMenu(){
        Debug.Log ("loading menu...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame(){
        Debug.Log("QUITing");
        Application.Quit();
    }
}
