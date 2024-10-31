using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static System.Threading.Tasks.Task;

public class BlinkingArrow : MonoBehaviour
{
    public bool isBlinking {get; set;}    
    public GameObject blinkingArrow;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void Blink()
    {
        while (isBlinking)
        {
            blinkingArrow.SetActive(true);
            await Delay(750);
            blinkingArrow.SetActive(false);
            await Delay(750);

            if (this == null) return;
        }
    }
    
}
