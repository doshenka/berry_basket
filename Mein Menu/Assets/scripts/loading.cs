using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loading : MonoBehaviour
{
    public string scene_2 = "level1"; // для перехода к сцене
    public GameObject progress_bar; // для всей загрузки
    public Image progress_bar_image; // спрайт 
    /*public Text loading_text; // процент загрузки*/
    public GameObject loading_image; // контейнер отдельный с загрузкой

    AsyncOperation async_operation; // куда сохраняется загрузка другой сцены

    void Start()
    {
        if(SceneManager.GetActiveScene().name == "loading") StartCoroutine("Async_load_press_key_COR", PlayerPrefs.GetString("current_scene")); //current заменить
    }

    //----------buttons

    // просто с заглушкой (чисто картинка)
    /*public void Simple_load_btn()
    {
        loading_image.SetActive(true);
        SceneManager.LoadScene(scene_2);
    }*/

    // асинхронная загрузка (с анимацией загрузки)
    public void Async_load_btn()
    {
        StartCoroutine("Async_load_COR");
    }

    // вернуться назад
    
    /*public void Reload_btn()
    {
        loading_image.SetActive(true);
        SceneManager.LoadScene(0);
    }*/

    //----загрузка

    //просто асинхр
    IEnumerator Async_load_COR()
    {
        float loading_progress;
        async_operation = SceneManager.LoadSceneAsync(scene_2);
        progress_bar.SetActive(true);
        loading_image.SetActive(true);
        while (!async_operation.isDone)
        {
            loading_progress = Mathf.Clamp01(async_operation.progress / 0.9f);
            /*loading_text.text = $"loading... {(loading_progress * 100).ToString("0")}%";*/
            progress_bar_image.fillAmount = loading_progress;
            yield return null;
        }
    }


    //асинхрон для сцены
}
