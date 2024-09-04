using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyBar : MonoBehaviour
{
    
    public DifficultManagerSO difficultyData;

    public Image barImage;

    private void OnEnable()
    {
        DifficultyEventSystem.UpdateDifficulty += UpdateUI;
    }

    private void OnDisable()
    {
        DifficultyEventSystem.UpdateDifficulty -= UpdateUI;
    }


    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void UpdateUI()
    {
        
        Debug.LogWarning("we changing level");
        barImage.fillAmount = SQLiteTest.PullDifficultyLevel(1) / 100;
    }
}
