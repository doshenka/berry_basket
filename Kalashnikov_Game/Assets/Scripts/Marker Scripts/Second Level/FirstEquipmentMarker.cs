using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FirstEquipmentMarker : MonoBehaviour
{
    public Animator animator;
    public PlayerMovement Player;
    public Avatar_Script avatar;
    public GameObject EndGamePanel;
    private float DestinationFromPlayer()
    {
        return Mathf.Sqrt(Mathf.Pow((Player.transform.position.x - transform.position.x), 2) + Mathf.Pow((Player.transform.position.y - transform.position.y), 2));
    }
    private IEnumerator LevelNextScene()
    {
        yield return new WaitUntil(() => DestinationFromPlayer() <= 20f);
        Player.SetTargetPosotion(new Vector2(Player.transform.position.x, Player.transform.position.y));
        Player.SetAgentPosition();
        animator.Play("Open Car Inventory");
        yield return new WaitForSeconds(1);
        avatar.SetText("–аботать на заводе нужно строго в спецодежде. ќна нужна дл€ защиты от воздействи€ опасных факторов. — ней физические и механические воздействи€ не страшны. ‘орма предохран€ет рабочего от неблагопри€тных условий.  роме того, она создает удобные услови€ дл€ работы. —отрудники чувствуют себ€ в ней уютно и защищенно. —пецодежда помогает справл€тьс€ со всеми своими об€занност€ми. ”дачного дн€!");
        avatar.TextType();
        Player.gameObject.GetComponent<Animator>().Play("Player_To_Equip");
        yield return new WaitUntil(() => avatar.gameObject.activeSelf == false);
        EndGamePanel.SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        PlayerPrefs.SetString("nextSceneName", "Main Menu");
        SceneManager.LoadScene("Load Menu");
        //gameObject.SetActive(false);
    }
    private void OnMouseDown()
    {
        Debug.Log(1);
        Player.SetTargetPosotion(new Vector2(transform.position.x, transform.position.y));
        Player.SetAgentPosition();
        StartCoroutine(LevelNextScene());
        Player.inPlayerController = false;
    }
}
