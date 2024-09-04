using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

/// <summary>
/// the purpose of this script is to indefinitely handle data life cycle of the difficulty bar from the
/// comfort of the persistant scene
/// </summary>
public class DifficultyManager : MonoBehaviour
{
    public float timeTillNextIncrease;
    public float difficultyLevelIncreaseValue;
    
    // Start is called before the first frame update
    void Start()
    {
        SQLiteTest.CreateNewDifficultyLevel();

        StartCoroutine(IncreaseDifficultyBarLevel());
        
        //SQLiteTest.UpdateDifficultyLevel(1, 50f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator IncreaseDifficultyBarLevel()
    {
        while (true)
        {
            if (SQLiteTest.PullDifficultyLevel(1) >= 95f)
            {
                SQLiteTest.UpdateDifficultyLevel(1, 100f);
                DifficultyEventSystem.OnUpdateDifficulty();
                yield return new WaitForSeconds(timeTillNextIncrease);    
            }
            else
            {
                float newDiffLevel = SQLiteTest.PullDifficultyLevel(1) + difficultyLevelIncreaseValue;
            
                Debug.LogWarning($"our new diff level is {newDiffLevel}");
            
                ActionsEventSystem.OnInitiateInjection(Random.Range(1, 2));
            
                SQLiteTest.UpdateDifficultyLevel(1, newDiffLevel);
                DifficultyEventSystem.OnUpdateDifficulty();
                yield return new WaitForSeconds(timeTillNextIncrease);    
            }
            
        }
    }
}
