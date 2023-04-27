using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 target;
    private Animator Animator;
    private Inventory_Manager inventory;
    private Items_Collection itemCollection;
    public Avatar_Script avatar;
    private NavMeshAgent agent;
    [SerializeField] private Camera Camera;
    private bool facingRight;
    private bool facingUp = true;
    private float prevPositionX, prevPositionY;
    public bool inPlayerController;
    private void WallChecker()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider == null || hit.collider.isTrigger)
            {
                SetTargetPosotion(worldPoint);
                SetAgentPosition();
            }
        }
    }
    private void Awake()
    {
        Camera = transform.GetChild(0).GetComponent<Camera>();
        Animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        inventory = GameObject.Find("Inventory Panel").GetComponent<Inventory_Manager>();
        itemCollection = GameObject.Find("Player Object").GetComponent<Items_Collection>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    private IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(2);
        avatar.GetComponent<Animator>().Play("Avatar Open");
        avatar.SetText("Приветствую, рабочий! Ты вот-вот отправишься на свой первый рабочий день на заводе. Осталось совсем немного! ДЛя того, чтобы попасть на завод, необходимо преодолеть две дороги. Не спеши, вспомни правила ПДД, прежде чем переходить дорогу.");
        avatar.TextType();
        yield return new WaitUntil(() => avatar.gameObject.activeSelf == false);
        inPlayerController = true;
        Animator.SetBool("LevelStart", true);
    }
    void Start()
    {
        StartCoroutine(CheckForMovement());
        Camera = GameObject.Find("Camera").GetComponent<Camera>();
        StartCoroutine(StartLevel());
    }
    private void Flip()
    {   
        facingRight = !facingRight;
        gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
    }
    public void SetTargetPosotion(Vector2 worldPoint)
    {
        target.x = worldPoint.x;
        target.y = worldPoint.y;
    }
    public void SetAgentPosition()
    {
        agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
    }
    private IEnumerator CheckForMovement()
    {
        prevPositionX = transform.position.x;
        prevPositionY = transform.position.y;

        yield return new WaitForSeconds(0.1f);

        if (prevPositionX == transform.position.x && prevPositionY == transform.position.y && target != new Vector3(0, 0, 0))
            target = new Vector3(0, 0, 0);

        StartCoroutine(CheckForMovement());
    }
    void Update()
    {
        if (inPlayerController)
            WallChecker();

        if (target.x != transform.position.x && target.y != transform.position.y && target != new Vector3(0, 0, 0) && target.y > transform.position.y)
        {
            Animator.SetBool("IsUpWalking", true);
            Animator.SetBool("IsDownWalking", false);
        }
        else if (target.x != transform.position.x && target.y != transform.position.y && target != new Vector3(0, 0, 0) && target.y < transform.position.y)
        {
            Animator.SetBool("IsUpWalking", false);
            Animator.SetBool("IsDownWalking", true);
        }
        else
        {
            Animator.SetBool("IsUpWalking", false);
            Animator.SetBool("IsDownWalking", false);
        }


        if (Math.Round(transform.position.x, 1, MidpointRounding.ToEven) == Math.Round(target.x, 1, MidpointRounding.ToEven) && Math.Round(transform.position.y, 1, MidpointRounding.ToEven) == Math.Round(target.y, 1, MidpointRounding.ToEven))
            target = new Vector3(0, 0, 0);

        if (facingRight && target.x > transform.position.x && !Animator.GetBool("IsDownWalking"))
        {
            Flip();
        }
        else if (!facingRight && target.x < transform.position.x && !Animator.GetBool("IsDownWalking"))
        { 
            Flip();
        }
        else if (facingRight && target.x < transform.position.x && Animator.GetBool("IsDownWalking"))
        {
            Flip();
        }
        else if (!facingRight && target.x > transform.position.x && Animator.GetBool("IsDownWalking"))
        {
            Flip();
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}