using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loading : MonoBehaviour
{
    public string scene_2 = "level1"; // для перехода к сцене
    public GameObject loading_line; // для всей загрузки
    public Image loading_line_image; // спрайт 
    public Text loading_precent; // процент загрузки
    public GameObject container_loading; // контейнер отдельный с загрузкой

    AsyncOperation async_operation; // куда сохраняется загрузка другой сцены

    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Loading_scene") StartCoroutine("Async_load_press_key_COR", PlayerPrefs.GetString("current_scene")); //current заменить
    }

    //----------buttons

    // просто с заглушкой (чисто картинка)
    /*public void Simple_load_btn()
    {
        container_loading.SetActive(true);
        SceneManager.LoadScene(scene_2);
    }*/

    // асинхронная загрузка (с анимацией загрузки)
    /*public void Async_load_btn()
    {
        StartCoroutine("Async_load_COR");
    }*/

    // асинхронная загрузка (со сценой)
    /*public void Scene_load_btn()
    {
        PlayerPrefs.SetString("current_scene", scene_2);
        SceneManager.LoadScene("Loading_scene");
    }

    public void Reload_btn()
    {
        container_loading.SetActive(true);
        SceneManager.LoadScene(0);
    }*/

    //----загрузка

    //просто асинхр
    /*IEnumerator Async_load_COR()
    {
        float load_progress;
        async_operation = SceneManager.LoadSceneAsync(scene_2);
        loading_line.SetActive(true);
        while(!async_operation.isDone)
        {
            load_progress = Mathf.Clamp01(async_operation.progress / 0.9f);
            loading_text.text = $"loading... {(load_progress * 100).ToString("0")}%"
        }
    }*/


    //асинхрон для сцены
}
