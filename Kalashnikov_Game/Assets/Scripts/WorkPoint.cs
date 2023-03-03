using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkPoint : MonoBehaviour
{
    public string workersAnimation;
    private Animator Animator;
    private List<Animation> Animations;
    public NavMeshAgent Agent;
    private void Start()
    {
        //Animations[0].Play();
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        Agent.enabled = true;
    }
}
