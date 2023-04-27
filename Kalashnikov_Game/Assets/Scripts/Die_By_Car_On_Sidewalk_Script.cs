using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Die_By_Car_On_Sidewalk_Script : MonoBehaviour
{
    private PlayerMovement Player;
    public Traffic_Lights_Manager Traffic_Lights;
    public Avatar_Script Avatar;
    public GameObject Car;
    public Vector3 finalPoint;

    private void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        Traffic_Lights = GameObject.Find("Traffic Manager").GetComponent<Traffic_Lights_Manager>();
    }
    private IEnumerator AvatarOpen()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("anim");
        Avatar.SetText("«апрещено переходить дорогу на красный сигнал светофора! ѕомните о правилах дорожного движени€, даже когда очень спешите.");
        Avatar.TextType();
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null && Traffic_Lights.current_state == "red")
        {
            Player.SetTargetPosotion(finalPoint);
            Player.SetAgentPosition();
            Player.inPlayerController = false;
        }
    }
    private void Update()
    {
        if (Math.Round(Player.gameObject.transform.position.x) == finalPoint.x && Math.Round(Player.gameObject.transform.position.y) == finalPoint.y && Traffic_Lights.current_state == "red")
        {
            Car.GetComponent<Animator>().Play("Move");
            StartCoroutine(AvatarOpen());
        }
    }
}
