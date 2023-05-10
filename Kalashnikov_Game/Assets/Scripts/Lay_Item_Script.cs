using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lay_Item_Script : MonoBehaviour
{
    private PlayerMovement Player;
    private Item_Panel_Script itemPanel;
    public Item item;
    private GameObject selectedOutline;
    private bool isMouseOn;
    private float DestinationFromPlayer()
    {
        return Mathf.Sqrt(Mathf.Pow((Player.transform.position.x - transform.position.x), 2) + Mathf.Pow((Player.transform.position.y - transform.position.y), 2));
    }
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        itemPanel = Player.itemPanel;
        selectedOutline = transform.GetChild(0).gameObject;

    }
    private void Update()
    {
        if (DestinationFromPlayer() <= 50 && !isMouseOn)
        {
            selectedOutline.SetActive(true);
            selectedOutline.GetComponent<SpriteRenderer>().color = new Color32(235, 230, 0, 255);
        }
        else if(DestinationFromPlayer() > 50)
            selectedOutline.SetActive(false);
    }
    private void OnMouseEnter()
    {
        isMouseOn = true;
        selectedOutline.GetComponent<SpriteRenderer>().color = new Color32(0, 255, 25, 255);
    }
    private void OnMouseExit()
    {
        isMouseOn = false;
    }
    private void OnMouseDown()
    {
        if (DestinationFromPlayer() < 50 && Player.avatar.gameObject.activeSelf == false)
        {
            Player.selectedLayItem = gameObject;
            itemPanel.itemName = item.itemName;
            itemPanel.itemSprite = item.sprite;
            itemPanel.itemDescription = item.description;
            Player.selectedItem = item;
            itemPanel.isOpen = true;
            itemPanel.animator.Play("Item_Panel_Default 0");
            Player.inPlayerController = false;
        }
    }
}
