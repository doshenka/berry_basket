using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayButtonScript : MonoBehaviour
{
    private void OnMouseDown()
    {
        PlayerPrefs.SetString("nextSceneName", "GameScene1");
        SceneManager.LoadScene("Load Menu");
    }
}

