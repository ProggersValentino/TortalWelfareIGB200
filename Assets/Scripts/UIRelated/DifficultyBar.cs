using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyBar : MonoBehaviour
{
    
    public DifficultManagerSO difficultyData;

    public Image barImage;

    // Bar Colour Gradient
    public Gradient difficultyBarGradient;

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

        //ChangeDifficultyBarColour();
        barImage.fillAmount = SQLiteTest.PullDifficultyLevel(1) / 100;
        ChangeDifficultyBarColour();
    }

    /// <summary>
    /// 
    /// </summary>
    public void ChangeDifficultyBarColour()
    {
        float barValue = barImage.fillAmount;
        if (barValue < 0)
        {
            barValue = 0;
        }
        barImage.color = difficultyBarGradient.Evaluate(barValue);
    }
}
