using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class load_script : MonoBehaviour
{
    /*public GameObject Container_Load;*/
    public Slider Slide_line;
    public float Timer;
    public string name_next_scene;

    /*// Start is called before the first frame update
    void Start()
    {
        
    }*/

    public void Async_load_btn()
    {
        Timer += Time.deltaTime;

        if ((Timer >= 2) && (Timer <= 4))
        {
            Slide_line.value = 15;
        }

        if ((Timer >= 4) && (Timer <= 8))
        {
            Slide_line.value = 30;
        }

        if ((Timer >= 8) && (Timer <= 10))
        {
            Slide_line.value = 60;
        }

        if ((Timer >= 10) && (Timer <= 12))
        {
            Slide_line.value = 75;
        }

        if ((Timer >= 12) && (Timer <= 14))
        {
            Slide_line.value = 90;
        }

        if ((Timer >= 14) && (Timer <= 16))
        {
            Slide_line.value = 100;
        }

        if (Timer >= 16)
        {
            Application.LoadLevel(name_next_scene);
        }

    }
}
