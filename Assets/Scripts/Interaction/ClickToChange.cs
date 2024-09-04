using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToChange : MonoBehaviour
{

    public string sceneToChange;
    int currentSceneID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadSceneAsync(sceneToChange);
    }
}
