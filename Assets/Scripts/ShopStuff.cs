using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopStuff : MonoBehaviour
{
    // Check money count when pressing buy
    // Changing text at bottom if not enough money
    // Update money if buying thing

    // Cost of the certificate
    public int certCost = 1000;

    // How many sand dollars
    int coinCount = 0;

    // Text on the shop menu that can change
    public TextMeshProUGUI certText;
    public TextMeshProUGUI messageText;

    // A surprise tool that will help me later
    public TextMeshProUGUI dollarCount;

    // Buttons to update
    public Button buyButton;
    public Button shopButtonMain;

    // UI to update
    public GameObject shopMenu;
    public GameObject winScreen;

    // Bool if cert has been bought already
    private bool certBought = false;

    private void OnEnable()
    {
        // Updates text, certificate cost, button interactableness
        
        certText.text = "Certificate of Beach Cleaning\n----\n" + certCost + " Sand Dollars";
        coinCount = SQLiteTest.PullPlayersMoney(1);

        if (certBought == false)
        {
            shopButtonMain.interactable = true;
            messageText.text = "";
            if (coinCount >= certCost)
            {
                buyButton.interactable = true;
                messageText.text = "You have enough sand dollars to buy the certificate!";
            }
            else
            {
                buyButton.interactable = false;
                messageText.text = "You don't have enough sand dollars yet!";
            }
        }
        else
        {
            shopButtonMain.interactable = false;
            messageText.text = "You've bought everything! Good job!";
        }
    }

    public void checkDollarAmount()
    {
        int newMoneyValue = 0;

        // Updating sand dollar amount
        newMoneyValue = coinCount - certCost;
        SQLiteTest.UpdatePlayersMoney(1, newMoneyValue);

        // Change bool
        certBought = true;

        // Update money on ranger screen (there must be a better way to do this but idk it)
        dollarCount.text = SQLiteTest.PullPlayersMoney(1).ToString();

        // Update buttons
        shopButtonMain.interactable = false;
        buyButton.interactable = false;

        // Update UI
        winScreen.SetActive(true);
        shopMenu.SetActive(false);
    }
}
