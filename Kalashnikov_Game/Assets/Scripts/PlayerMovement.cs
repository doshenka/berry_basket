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

    // объекты первого уровня
    public GameObject carInventoryMarker;
    public GameObject firstStepMarker;

    // объекты второго уровня
    public GameObject gearBoxMarker;

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
        yield return new WaitForSeconds(0.5f);
        avatar.GetComponent<Animator>().Play("Avatar Open");
        avatar.SetText("Приветствую, коллега! Сегодня ваш первый рабочий день!На заводе необходимо знать правила техники безопасности. И сейчас вам предстоит с ними ознакомиться! (Передвигайтесь, кликая на место, куда хотите отправиться).");
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
        avatar.SetText("Вот-вот вы уже отправитесь на работу! Немного осталось! Чтобы попасть на Концерн, не забудьте взять пропуск. Нажмите на лупу над вашей машиной и заберите его.Просто кликните на появившийся пропуск.");
        avatar.TextType();
        yield return new WaitUntil(() => getWorkPass == true);
        inPlayerController = false;
        carInventoryMarker.SetActive(false);
        StartCoroutine(FirstStepMovement());
    }
    public IEnumerator FirstStepMovement()
    {
        avatar.GetComponent<Animator>().Play("Avatar Open");
        avatar.SetText("Отлично! Теперь нужно подойти к пешеходному переходу. Нажмите на маркер с восклицательным знаком.");
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
        avatar.SetText("Класс! Осталось последнее задание - надо перейти две дороги. Не спеши, вспомни правила ПДД и будь внимателен!");
        avatar.TextType();
        yield return new WaitUntil(() => avatar.gameObject.activeSelf == false);
        inPlayerController = true;
    }
    // события уровня 2
    private IEnumerator StartLevel_2()
    {
        inventory.GetItem(itemCollection.itemCollection[0]);
        inventory.GetItem(itemCollection.itemCollection[1]);
        yield return new WaitForSeconds(0.5f);
        avatar.GetComponent<Animator>().Play("Avatar Open");
        avatar.SetText("Добро пожаловать на Концерн Калашников! Сейчас вам предстоит выполнить здесь первое задание. Используйте ваш пропуск и преодолейте КПП. (Кликните на логотип Концерна).");
        avatar.TextType();
        yield return new WaitUntil(() => avatar.gameObject.activeSelf == false);
        Animator.SetBool("LevelStart", true);
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(0.5f);
        gearBoxMarker.SetActive(true);
        inPlayerController = true;
    }
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
        else if (SceneManager.GetActiveScene().name == "FirstFactoryScene")
        {
            StartCoroutine(StartLevel_2());
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
        if (Input.GetKeyDown(KeyCode.Space))
            avatar.TypeFullText();

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