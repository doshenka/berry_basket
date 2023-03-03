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
        if(SceneManager.GetActiveScene().name == "Scene_loading") StartCoroutine("Async_load_press_key_COR", PlayerPrefs.GetString("current_scene"));
    }

    // просто с заглушкой
    public void Simple_load_btn()
    {
        container_loading.SetActive(true);
        //SceneManager.LoadScene;
    }
}
