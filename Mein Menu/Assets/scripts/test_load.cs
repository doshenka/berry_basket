using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test_load : MonoBehaviour
{
    public GameObject Load_akademy;
    public GameObject Load_koncern;

    public Slider Slide_line;
    public float Timer;

    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;

        if ((Timer >= 2) && (Timer <= 4))
        {
            Load_koncern.SetActive(false);
        }

        if ((Timer >= 4) && (Timer <= 8))
        {
            Slide_line.value = 20;
        }

        if ((Timer >= 8) && (Timer <= 10))
        {
            Slide_line.value = 50;
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
            Application.LoadLevel("menu");
        }
    }
}
