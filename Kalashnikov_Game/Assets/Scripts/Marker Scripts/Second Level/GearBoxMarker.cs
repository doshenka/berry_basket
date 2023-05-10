using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearBoxMarker : MonoBehaviour
{
    private PlayerMovement Player;
    private Avatar_Script avatar;
    private void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        avatar = Player.avatar;
    }
    private IEnumerator GearBoxActions()
    {
        yield return new WaitForSeconds(1);
        Player.gearBoxMarker.GetComponent<Animator>().Play("Open Car Inventory");
        yield return new WaitForSeconds(0.5f);
        Player.gearBoxMarker.SetActive(false);
        Player.SetTargetPosotion(new Vector2(-414, 101));
        Player.SetAgentPosition();
        yield return new WaitForSeconds(1.5f);
        avatar.SetText("Здорово! Следующая задача - надеть специальную форму.Пройдите к шкафчикам и кликните на жёлтую каску. (Раздевалка расположена слева от входа).");
        avatar.TextType();
        Player.inPlayerController = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            Player.inPlayerController = false;
            Player.SetTargetPosotion(new Vector2(-429, 90));
            Player.SetAgentPosition();
            StartCoroutine(GearBoxActions());
        }
    }
}
