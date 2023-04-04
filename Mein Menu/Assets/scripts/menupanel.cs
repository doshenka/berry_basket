using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menupanel : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Settings;
    public GameObject Developers;

    /*[SerializeField] private GameObject _btnPlay;*/

    public void StartGame()
    {
        Application.LoadLevel("level1");
    }

    public void MenuChange()
    {
        Menu.SetActive(true);
        Settings.SetActive(false);
        Developers.SetActive(false);
    }

    public void SettingsChange()
    {
        Menu.SetActive(false);
        Settings.SetActive(true);
        Developers.SetActive(false);
    }

    public void DevelopersChange()
    {
        Menu.SetActive(false);
        Developers.SetActive(true);
        Settings.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    /*public void SetObject()
    {
        StartCoroutine(HideObject());
        Debug.Log("Hey");
    }
    IEnumerator HideObject()
    {
        _btnPlay.SetActive(false);
        yield return new WaitForSeconds(5.0f);
        _btnPlay.SetActive(true);
    }*/
}