using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Script : MonoBehaviour
{
    private PlayerMovement Player;
    private void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            Player.GetComponent<Animator>().Play("Die By Car");
        }
    }
}
