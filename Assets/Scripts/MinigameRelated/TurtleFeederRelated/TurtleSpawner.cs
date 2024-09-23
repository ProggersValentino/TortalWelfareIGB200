using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleSpawner : Spawner
{
    
    // Start is called before the first frame update
    void Start()
    {
        SummonTortal();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// summon hungry tortal
    /// </summary>
    public void SummonTortal()
    {
        Vector3 locationToGo = new Vector3(Random.Range(point1.position.x, point2.position.x), point1.position.y,
            point1.position.z);
        GameObject newTurtle = Instantiate(turtlePref, transform.position, turtlePref.transform.rotation);

        newTurtle.transform.SetParent(transform, true);
        
        HungryTurtle ht = newTurtle.GetComponent<HungryTurtle>();

        ht.SetNewDestination(locationToGo);
    }   
}
