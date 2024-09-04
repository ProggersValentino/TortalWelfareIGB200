using System.Collections;
using System.Collections.Generic;
using Unity.Loading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MultipleSceneManager : MonoBehaviour
{
    //public SceneField persistentScene;
    public List<SceneField> ScenesToLoad = new List<SceneField>();
    public List<SceneField> ScenesToUnload = new List<SceneField>();
    
    private List<AsyncOperation> _scenesLoadQueue = new List<AsyncOperation>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScenes()
    {
        foreach (SceneField scene in ScenesToLoad)
        {
            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            Debug.LogWarning(SceneManager.GetActiveScene().name);
        }
        
       // UnloadScenes();
    }
    
    public void UnloadScenes()
    {
        foreach (SceneField scene in ScenesToUnload)
        {
            SceneManager.UnloadSceneAsync(scene);
            
            
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitUntilLoaded()
    {
        for (int i = 0; i < _scenesLoadQueue.Count; i++)
        {
            //_scenesLoadQueue[i].allowSceneActivation = false;
            while (!_scenesLoadQueue[i].isDone)
            {
                Debug.LogWarning("waiting to load");
                
                // if (_scenesLoadQueue[i].progress >= 0.9f)
                // {
                //     _scenesLoadQueue[i].allowSceneActivation = true;
                // }
                
                yield return null;
            }
            
        }
        Debug.LogWarning("loaded");
        UnloadScenes();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(ScenesToLoad[0]));
        
        
        //Debug.LogWarning(SceneManager.GetActiveScene().name);
    }
    
    public void SetNewSceneAsActive()
    {
        
        foreach (SceneField scene in ScenesToLoad)
        {
            _scenesLoadQueue.Add(SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive));    
        }


        StartCoroutine(WaitUntilLoaded());
        
    }
}
