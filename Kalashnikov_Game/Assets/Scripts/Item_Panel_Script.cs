using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Panel_Script : MonoBehaviour
{
    public Sprite itemSprite;
    public Image itemImage;
    public string itemName;
    public string itemDescription;
    public Animator animator;
    public bool isOpen;

    public GameObject itemNameText;
    public GameObject itemDescriptionText;

    private PlayerMovement Player;
    private Inventory_Manager inventory;

    private void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        inventory = Player.inventory;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isOpen)
        {
            animator.Play("Item Panel Close");
            Player.selectedItem = null;
            Player.inPlayerController = true;
        }
        if(Input.GetKeyDown(KeyCode.F) && isOpen)
        {
            if (itemName == "опносяй")
                Player.getWorkPass = true;
            inventory.GetItem(Player.selectedItem);
            animator.Play("Item Panel Close");
            Player.selectedItem = null;
            isOpen = false;
            Destroy(Player.selectedLayItem);
            Player.inPlayerController = true;
        }
        itemNameText.GetComponent<TMPro.TextMeshProUGUI>().text = itemName;
        itemDescriptionText.GetComponent<TMPro.TextMeshProUGUI>().text = itemDescription;
        itemImage.sprite = itemSprite;
    }
}
