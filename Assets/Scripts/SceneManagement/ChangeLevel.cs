using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeLevel : MonoBehaviour
{

    int currentSceneID;
    //public StringSO sceneNameListener;

    // Start is called before the first frame update
    void Start()
    {
        currentSceneID = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        
    }

    public void ChangeScene(SceneSO sceneChangeData)
    {
        //SceneManager.UnloadSceneAsync(currentSceneID);
        SceneManager.LoadSceneAsync(sceneChangeData.sceneNameToChange);
    }
}
