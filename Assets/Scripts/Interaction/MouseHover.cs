using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseHover : MonoBehaviour
{
    public UnityEvent onInteraction;

    //public SceneSO stringSO;

    public bool wantsToGo;

    public Transform predeterminedLoco;

    GameObject player;

    private bool hasClicked = false;
    
    private void OnEnable()
    {
        //onInteraction.AddListener(stringSO.ChangeScene);
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        wantsToGo = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && wantsToGo)
        {
            //onInteraction?.Invoke();
        }
    }*/

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Vector3.Distance(other.gameObject.transform.position, predeterminedLoco.position) <= 0.5f && !hasClicked)
            {
                hasClicked = true;
                onInteraction?.Invoke(); 
            }
        }
    }

    private void OnMouseOver()
    {
       /* if (Input.GetMouseButtonDown(0)) wantsToGo = true;*/
        Debug.Log($"we are over {transform.gameObject}");
    }

    private void OnMouseExit()
    {
        wantsToGo = false;
    }
}
