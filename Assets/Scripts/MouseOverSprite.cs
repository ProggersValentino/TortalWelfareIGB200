using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverSprite : MonoBehaviour
{
    public GameObject spriteCollider;
    public Material noOutlineMat;
    public Material outlineMat;

    public GameObject spriteEnable;

    private void OnMouseOver()
    {
        spriteCollider.GetComponent<SpriteRenderer>().material = outlineMat;
        if (spriteEnable != null)
        {
            spriteEnable.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        spriteCollider.GetComponent<SpriteRenderer>().material = noOutlineMat;
        if (spriteEnable != null)
        {
            spriteEnable.SetActive(false);
        }
    }
}
