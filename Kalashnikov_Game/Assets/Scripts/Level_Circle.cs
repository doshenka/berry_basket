using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Circle : MonoBehaviour
{
    private float DestinationFromPlayer()
    {
        return Mathf.Sqrt(Mathf.Pow((Player.transform.position.x - transform.position.x), 2) + Mathf.Pow((Player.transform.position.y - transform.position.y), 2));
    }

    [SerializeField] private GameObject Player;
    [SerializeField] private string SceneName;
    public GameObject EndFirstLevelPanel;
    /*
    void OnMouseEnter()
    {
        if (DestinationFromPlayer() < 30)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
    void OnMouseDown()
    {
        if (DestinationFromPlayer() < 2)
            SceneManager.LoadScene(SceneName);
    }
    */
    private IEnumerator NextLevel()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        PlayerPrefs.SetString("nextSceneName", SceneName);
        SceneManager.LoadScene("Load Menu");
    }
    void Update()
    {
        
        if(DestinationFromPlayer() <= 2)
        {
            Player.GetComponent<PlayerMovement>().inPlayerController = false;
            EndFirstLevelPanel.SetActive(true);
            StartCoroutine(NextLevel());
        }
        else if (DestinationFromPlayer() < 8)
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        else
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
    /*
    void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
    */
}