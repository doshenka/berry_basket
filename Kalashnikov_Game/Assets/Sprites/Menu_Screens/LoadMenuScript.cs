using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenuScript : MonoBehaviour
{
    public string nextScene;
    public GameObject text;
    void Start()
    {
        nextScene = PlayerPrefs.GetString("nextSceneName");
        StartCoroutine(NextScene());
    }
    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(8);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        text.GetComponent<TMPro.TextMeshProUGUI>().text = "«¿√–”« ¿...";
        SceneManager.LoadScene(nextScene);
    }
}
