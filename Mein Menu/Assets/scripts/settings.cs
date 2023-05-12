using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settings : MonoBehaviour
{
    public Sprite music_on, music_off;

    /*void Start () {
        if (gameObject.name == "Music") {
            if (PlayerPrefs.GetString () == "off") {
                GetComponent <Image> ().sprite = music_off;
                //PlayerPrefs.SetString ("Music", "off"); ПЕРЕПИСАТЬ С КАМЕРОЙ И ОТКЛ МУЗЫКИ
            } else {
                GetComponent <Image> ().sprite = music_on;
                PlayerPrefs.SetString ("Music", "on");
            }
        }
    }

    void SettingsBtn (){
        switch (gameObject.name) {
            case "Music":
                if (PlayerPrefs.GetString ("Music") == "off") {
                    GetComponent <Image> ().sprite = music_on;
                    PlayerPrefs.SetString ("Music", "on");
                } else {
                    GetComponent <Image> ().sprite = music_off;
                    PlayerPrefs.SetString ("Music", "off");
                }
            /*case "Bright":
                ;*/
        /*}
    }*/
}
