using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInventoryMarker : MonoBehaviour
{
    public Animator animator;
    private void OnMouseDown()
    {
        animator.Play("Open Car Inventory");
    }
}
