using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public Animator Animator;
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    private void OnMouseEnter()
    {
        Animator.SetBool("Mouse", true);
    }
    void OnMouseExit()
    {
        Animator.SetBool("Mouse", false);
    }
    void Update()
    {
        
    }
}
