using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodSpawner : Spawner
{

    public List<Sprite> potentialFoodSprite = new List<Sprite>();
    public List<Sprite> potentialTrashSprite = new List<Sprite>();
    
    public GameObject rubbishRef;
    
    [Range(0, 2)]
    public float multiplier;
    
    public float spawnPerSeconds;
    
    
    private float percentageChance = 1f;

    private void Awake()
    {
        percentageChance = SQLiteTest.PullDifficultyLevel(2);
    }

    // Start is called before the first frame update
    void Start()
    {
        //SummonFood(turtlePref);
        StartCoroutine(SpawnPerSection());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /// <summary>
    /// summon food
    /// </summary>
    public void SummonFood(GameObject pref, Sprite sprite)
    {
        Vector3 locationToGo = new Vector3(point1.position.x, point1.position.y,
            Random.Range(point1.position.z, point2.position.z));
        GameObject newFood = Instantiate(pref, transform.position, turtlePref.transform.rotation);

        SpriteRenderer foodSprite = newFood.GetComponentInChildren<SpriteRenderer>();
        foodSprite.sprite = sprite;
        
        newFood.transform.SetParent(transform, true);
        
        Food food = newFood.GetComponent<Food>();

        food.SetNewDestination(locationToGo);
    }

    IEnumerator SpawnPerSection()
    {
        while (isGameGoing)
        {
            percentageChance *= multiplier;

            float roll = Random.Range(0, 100);
            Debug.LogWarning($"we have rolled {roll} with the percentage of {percentageChance}");
            if(roll < percentageChance) //spawn trash
            {
                Sprite spriteToPut = potentialFoodSprite[Random.Range(0, potentialFoodSprite.Count - 1)];
                Debug.LogWarning("Summon rubbish");
                SummonFood(rubbishRef, spriteToPut);
            }
            else
            {
                Sprite spriteToPut = potentialTrashSprite[Random.Range(0, potentialTrashSprite.Count - 1)];
                SummonFood(turtlePref, spriteToPut); //spawn food}
            }

            yield return new WaitForSeconds(spawnPerSeconds);
        }
    }
    
}
