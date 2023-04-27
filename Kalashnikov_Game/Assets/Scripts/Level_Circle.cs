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

    void Update()
    {
        if (DestinationFromPlayer() < 2)
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }

    void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}