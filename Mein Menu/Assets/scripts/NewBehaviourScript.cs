using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    [SerializeField] private GameObject _test1;
    [SerializeField] private GameObject _test2;

    public void SetObject()
    {
        StartCoroutine(HideObject()); 
        Debug.Log("Hey");
    }
    IEnumerator HideObject()
    {
        _test1.SetActive(false);
        yield return new WaitForSeconds(5.0f);
        _test1.SetActive(true);
    }

    public void SetSettings()
    {
        StartCoroutine(HideSettings()); 
        Debug.Log("Settings");
    }

    IEnumerator HideSettings()
    {
        _test2.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        _test2.SetActive(true);
    }


}
