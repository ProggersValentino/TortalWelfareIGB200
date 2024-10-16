using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverSprite : MonoBehaviour
{
    public GameObject spriteCollider;
    public Material noOutlineMat;
    public Material outlineMat;

    private void OnMouseOver()
    {
        spriteCollider.GetComponent<SpriteRenderer>().material = outlineMat;
    }

    private void OnMouseExit()
    {
        spriteCollider.GetComponent<SpriteRenderer>().material = noOutlineMat;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
