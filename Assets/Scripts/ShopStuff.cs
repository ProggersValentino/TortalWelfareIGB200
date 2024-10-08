using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopStuff : MonoBehaviour
{
    // Check money count when pressing buy
    // Changing text at bottom if not enough money
    // Update money if buying thing

    // Cost of the certificate
    public int certCost = 1000;
    int coinCount = 0;
    public TextMeshProUGUI messageText;

    // Start is called before the first frame update
    void Start()
    {
        messageText.text = "Press the image to buy!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkDollarAmount()
    {
        coinCount = SQLiteTest.PullPlayersMoney(1);
        if (coinCount >= certCost)
        {
            // Do all the menu changing and updating money
        }
        else
        {
            messageText.text = "Not enough sand dollars!";
        }
    }
}
