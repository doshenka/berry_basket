using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour
{
    public string name;
    public List<WorkPoint> workPoints;
    private NavMeshAgent Agent;
   
    private float distanceToNextPoint;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private int pointIndex = 0;
    private Animator Animator;
    private List<Animation> WorkbenchAnimations;
    private void goToNextPoint()
    {
        pointIndex = Mathf.Min(pointIndex, workPoints.Count);
    }
    private IEnumerator NextPoint()
    {
        Animator.Play(workPoints[pointIndex % workPoints.Count].workersAnimation);
        if (workPoints[pointIndex % workPoints.Count].workersAnimation == "")
        {
            yield return new WaitForSeconds(1);
            StopAllCoroutines();
            pointIndex++;
        }
        else if (!Animator.GetCurrentAnimatorStateInfo(0).IsName(workPoints[pointIndex % workPoints.Count].workersAnimation))
        {
            pointIndex++;
            StopAllCoroutines(); 
        }
    }

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        Agent.enabled = true;
    }

    private void FixedUpdate()
    {
        if (workPoints.Count > 0)
        {
            target = workPoints[pointIndex % workPoints.Count].transform;
            distanceToNextPoint = Vector3.Distance(target.position, transform.position);
            if (distanceToNextPoint > 1)
            {
                Agent.enabled = true;
                Agent.SetDestination(target.position);
            }
            else
            {
                Agent.enabled = false;
                StartCoroutine(NextPoint());
            }
        }
    }
}
