using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetSceneActive : MonoBehaviour
{
    public MultipleSceneManager yes;
    
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Indef());
    }


    IEnumerator Indef()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            Debug.LogWarning("increasing diff");
        }
    }
    
    public void Yes()
    {
        Scene[] loadedScenes = SceneManager.GetAllScenes();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(loadedScenes[1].name));  
        Debug.LogWarning(SceneManager.GetActiveScene().name);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
