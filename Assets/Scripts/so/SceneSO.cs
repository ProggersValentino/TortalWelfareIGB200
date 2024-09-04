using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


[CreateAssetMenu(menuName = "SceneManagement/New Scene Change")]
public class SceneSO : ScriptableObject
{

    public string sceneNameToChange;
    int currentSceneID;
    public UnityAction onSceneEventRaised;

 /*   public void EventRaised(string sceneAction)
    {
        onSceneEventRaised?.Invoke(sceneAction);
    }*/

    public void ChangeScene()
    {
        //SceneManager.UnloadSceneAsync(currentSceneID);
        SceneManager.LoadSceneAsync(sceneNameToChange);
    }
}
