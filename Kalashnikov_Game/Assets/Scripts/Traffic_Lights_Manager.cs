using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traffic_Lights_Manager : MonoBehaviour
{
    private string[] states = { "red", "green" };
    private int state_value = 0;
    public string current_state;
    private IEnumerator Timer()
    {
        current_state = states[state_value];
        yield return new WaitForSeconds(10);
        state_value++;
        state_value %= 2;
        StartCoroutine(Timer());
    }
    public void SetState(int newState)
    {
        StopCoroutine(Timer());
        state_value = newState;
        StartCoroutine(Timer());
    }
    void Start()
    {
        StartCoroutine(Timer());
    }
}
