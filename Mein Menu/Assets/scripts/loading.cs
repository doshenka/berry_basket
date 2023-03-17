using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loading : MonoBehaviour
{
    public string scene_2 = "level1"; // для перехода к сцене
    public GameObject progress_bar; // для всей загрузки
    public Image progress_bar_image; // спрайт 
    public Text loading_text; // процент загрузки
    public GameObject loading_image; // контейнер отдельный с загрузкой

    AsyncOperation async_operation; // куда сохраняется загрузка другой сцены

    void Start()
    {
        if(SceneManager.GetActiveScene().name == "loading") StartCoroutine("Load_press_key_COR", PlayerPrefs.GetString("current_scene"));
    }

    // просто с заглушкой
    public void Load_btn()
    {
        StartCoroutine("Load_COR");
    }

    public void Reload_btn()
    {
        loading_image.SetActive(true);
        SceneManager.LoadScene(0);
    }

    IEnumerator Load_press_key_COR(string scene_name)
    {
        float loading_progress;
        async_operation = SceneManager.LoadSceneAsync(scene_2);
        progress_bar.SetActive(true);
        while(!async_operation.isDone)
        {
            loading_progress = Mathf.Clamp01(async_operation.progress / 0.9f);
            loading_text.text = $"Loading ... {(loading_progress * 100).ToString("0")}%";
            progress_bar_image.fillAmount = loading_progress;
            yield return null;
        }
    }
}
