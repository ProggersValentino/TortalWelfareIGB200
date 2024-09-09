using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpTurtleMicroTask : MicroTask
{
    public GameObject turtleHelpUI;

    public GameObject[] trashContainer;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMicroGame()
    {
        turtleHelpUI.SetActive(true);
    }

    public void EndOfMicroGame()
    {
        int amountGone = 0;
        
        foreach (GameObject trash in trashContainer)
        {
            if (trash.activeSelf)
            {
                amountGone++;
            }
        }
        
        if(amountGone > 0) ProcessTaskCompletion(taskData._difficultyIncreaseLevel);
        else ProcessTaskCompletion(taskData._difficultyDecreaseLevel);
        
        Destroy(this.gameObject);
        
        
    }
}
