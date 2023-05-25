using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Die_By_Car_On_Sidewalk_Script : MonoBehaviour
{
    private PlayerMovement Player;
    public Traffic_Lights_Manager Traffic_Lights;
    public Avatar_Script avatar;
    public GameObject Car;
    public Vector3 finalPoint;

    private void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        Traffic_Lights = GameObject.Find("Traffic Manager").GetComponent<Traffic_Lights_Manager>();
        StartCoroutine(AvatarOpen());
    }
    private IEnumerator AvatarOpen()
    {
        yield return new WaitUntil(() => Math.Round(Player.gameObject.transform.position.x) == finalPoint.x && Math.Round(Player.gameObject.transform.position.y) == finalPoint.y && Traffic_Lights.current_state == "red");
        Car.GetComponent<Animator>().Play("Move");
        yield return new WaitForSeconds(1);
        Debug.Log("anim");
        avatar.SetText("«апрещено переходить дорогу на красный сигнал светофора! ѕомните о правилах дорожного движени€, даже когда очень спешите.");
        avatar.TextType();
        yield return new WaitUntil(() => avatar.gameObject.activeSelf == false);
        SceneManager.LoadScene("GameScene1");
        PlayerPrefs.SetString("SkippingFirstSceneDialogs", "true");
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
}
