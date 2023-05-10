using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStepMarkerScript : MonoBehaviour
{
    public Animator animator;
    private IEnumerator LevelNextScene()
    {
        animator.Play("Open Car Inventory");
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
    private void OnMouseDown()
    {
        StartCoroutine(LevelNextScene());
    }
}
