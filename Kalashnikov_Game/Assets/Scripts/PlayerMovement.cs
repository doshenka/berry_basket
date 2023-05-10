using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public GameObject selectedLayItem;
    public Vector3 target;
    private Animator Animator;
    private Rigidbody2D rigidbody;
    public Inventory_Manager inventory;
    private Items_Collection itemCollection;
    public Avatar_Script avatar;
    private NavMeshAgent agent;
    [SerializeField] private Camera Camera;
    private bool facingRight;
    private bool facingUp = true;
    private float prevPositionX, prevPositionY;
    public bool inPlayerController;
    public Item_Panel_Script itemPanel;
    public Item selectedItem;

    public GameObject carInventoryMarker;
    public GameObject firstStepMarker;

    public bool getWorkPass;
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
        rigidbody = GetComponent<Rigidbody2D>();
        inventory = GameObject.Find("Inventory Panel").GetComponent<Inventory_Manager>();
        itemCollection = GameObject.Find("Player Object").GetComponent<Items_Collection>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // список событий уровней. перенести в отдельный файл для удобства работы с кодом (потом)
    // события уровня 1
    private IEnumerator StartLevel_1()
    {
        //itemPanel.GetComponent<Animator>().Play("Item_Panel_Default 0"); открытие панели описания предмета
        yield return new WaitForSeconds(0.5f);
        avatar.GetComponent<Animator>().Play("Avatar Open");
        //avatar.SetText("Приветствую, рабочий! Ты вот-вот отправишься на свой первый рабочий день на заводе. Осталось совсем немного! Для того, чтобы попасть на завод, необходимо преодолеть две дороги. Не спеши, вспомни правила ПДД, прежде чем переходить дорогу.");
        avatar.SetText("a");
        avatar.TextType();
        yield return new WaitUntil(() => avatar.gameObject.activeSelf == false);
        Animator.SetBool("LevelStart", true);
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(0.5f);
        carInventoryMarker.SetActive(true);
        StartCoroutine(FirstCarInteraction());
    }
    public IEnumerator FirstCarInteraction()
    {
        avatar.GetComponent<Animator>().Play("Avatar Open");
        avatar.SetText("Нажмите на иконку лупы над вашей машиной и возьмите появишвийся пропуск, кликнув на него.");
        avatar.TextType();
        yield return new WaitUntil(() => getWorkPass == true);
        inPlayerController = false;
        carInventoryMarker.SetActive(false);
        StartCoroutine(FirstStepMovement());
    }
    public IEnumerator FirstStepMovement()
    {
        avatar.GetComponent<Animator>().Play("Avatar Open");
        avatar.SetText("Отлично! Теперь пройдите к указанной точке, нажам на марекр со знаком '!'.");
        avatar.TextType();
        yield return new WaitUntil(() => avatar.gameObject.activeSelf == false);
        firstStepMarker.SetActive(true);
        inPlayerController = true;
        StartCoroutine(TrafficLightScene());
    }
    public IEnumerator TrafficLightScene()
    {
        yield return new WaitUntil(() => firstStepMarker.activeSelf == false);
        inPlayerController = false;
        avatar.SetText("Класс! Остался последний шаг - перейти дорогу. Будь внимателен и не нарушай ПДД!");
        avatar.TextType();
        yield return new WaitUntil(() => avatar.gameObject.activeSelf == false);
        inPlayerController = true;
    }    
    // события уровня 2
    // конец списка событий

    void Start()
    {
        StartCoroutine(CheckForMovement());
        Camera = GameObject.Find("Camera").GetComponent<Camera>();
        if (SceneManager.GetActiveScene().name == "GameScene1")
        {
            StartCoroutine(StartLevel_1());
            carInventoryMarker.SetActive(false);
            firstStepMarker.SetActive(false);
        }
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

        if (!Animator.GetBool("IsDownWalking") && !facingRight && target.x < transform.position.x)
        {
            Flip();
        }
        else if (!Animator.GetBool("IsDownWalking") && facingRight && target.x > transform.position.x)
        { 
            Flip();
        }
        else if (Animator.GetBool("IsDownWalking") && facingRight && target.x < transform.position.x)
        {
            Flip();
        }
        else if (Animator.GetBool("IsDownWalking") && !facingRight && target.x > transform.position.x)
        {
            Flip();
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}