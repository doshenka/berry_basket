using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Default_Car_Script : MonoBehaviour
{
    public Sprite[] sprites;
    public int spriteCount;
    void Start()
    {
        System.Random r = new System.Random();
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[r.Next(0, spriteCount)];   
    }
}
