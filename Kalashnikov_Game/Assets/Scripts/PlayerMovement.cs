using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 target;
    NavMeshAgent agent;
    public Camera Camera;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    void Start()
    {
        
    }
    void SetTargetPosotion()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = Camera.ScreenToWorldPoint(Input.mousePosition);
        }
    }
    void SetAgentPosition()
    {
        agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
    }
    void Update()
    {
        SetTargetPosotion();
        SetAgentPosition();
    }
}
