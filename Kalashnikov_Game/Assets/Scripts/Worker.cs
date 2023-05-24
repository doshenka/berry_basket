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
        
        
        //Animator.Play(workPoints[pointIndex % workPoints.Count].workersAnimation);
        /*
        if (workPoints[pointIndex % workPoints.Count].workersAnimation == "")
        {
            yield return new WaitForSeconds(1);
            pointIndex++;
            //StopCoroutine(NextPoint());
            
        }
        else if (!Animator.GetCurrentAnimatorStateInfo(0).IsName(workPoints[pointIndex % workPoints.Count].workersAnimation))
        {
            yield return new WaitForSeconds(1);
            pointIndex++;
            //StopCoroutine(NextPoint());
        }
        */
        yield return new WaitForSeconds(10);
        pointIndex++;
        Agent.SetDestination(target.position);
        StartCoroutine(NextPoint());
    }

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        Agent.enabled = true;
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        Animator.Play("Stand_Animation");
        StartCoroutine(NextPoint());
    }

    private void FixedUpdate()
    {
        target = workPoints[pointIndex % workPoints.Count].transform;
        distanceToNextPoint = Vector3.Distance(target.position, transform.position);
        
        /*
        if (distanceToNextPoint > 0.1f)
        {
            Agent.enabled = true;
            Agent.SetDestination(target.position);
        }
        */
        /*
        else
        {
            Agent.enabled = false;
        }
        */
        if (target.position.x != transform.position.x && target.position.y != transform.position.y && target.position != new Vector3(0, 0, 0) && target.position.y > transform.position.y)
        {
            Animator.SetBool("IsUpWalking", false);
            Animator.SetBool("IsDownWalking", true);
        }
        else if (target.position.x != transform.position.x && target.position.y != transform.position.y && target.position != new Vector3(0, 0, 0) && target.position.y < transform.position.y)
        {
            Animator.SetBool("IsUpWalking", true);
            Animator.SetBool("IsDownWalking", false);
        }
        else
        {
            Animator.SetBool("IsUpWalking", false);
            Animator.SetBool("IsDownWalking", false);
        }
    }
}
