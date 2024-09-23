using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : Spawner
{
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        SummonFood();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /// <summary>
    /// summon hungry tortal
    /// </summary>
    public void SummonFood()
    {
        Vector3 locationToGo = new Vector3(point1.position.x, point1.position.y,
            Random.Range(point1.position.z, point2.position.z));
        GameObject newFood = Instantiate(turtlePref, transform.position, turtlePref.transform.rotation);

        newFood.transform.SetParent(transform, true);
        
        Food food = newFood.GetComponent<Food>();

        food.SetNewDestination(locationToGo);
    }   
    
}
