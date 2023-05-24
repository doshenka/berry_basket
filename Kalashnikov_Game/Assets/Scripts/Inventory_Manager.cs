using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Manager : MonoBehaviour
{
    public int bagCapacity;
    public int currentItemIndex;
    public Item[] itemBag;
    public Image[] itemImages; 
    private void Start()
    {
        itemBag = new Item[bagCapacity];
        itemImages = new Image[bagCapacity];
        for(int i = 0; i < bagCapacity; i++)
        {
            itemImages[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }
    private void UpdateInventory()
    {
        for(int i = 0; i < bagCapacity; i++)
        {
            if (itemBag[i] != null)
            {
                itemImages[i].transform.GetChild(0).GetComponent<Image>().sprite = itemBag[i].sprite;
            }
            else
                itemImages[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
            if (i == currentItemIndex)
                itemImages[i].transform.GetChild(1).gameObject.SetActive(true);
            else
                itemImages[i].transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    public void GetItem(Item newItem)
    {
        for(int i = 0; i < bagCapacity; i++)
        {
            if (itemBag[i] == null)
            {
                itemBag[i] = newItem;
                UpdateInventory();
                Debug.Log($"Item {newItem.itemName} has successfully added to inventory");
                return;
            }
        }
    }
    public void DeleteItemFromCell(int id)
    {
        itemBag[id] = null;
        UpdateInventory();
    }
    private void CurrentItemChanging()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentItemIndex++;
            currentItemIndex %= bagCapacity;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentItemIndex--;
            if (currentItemIndex < 0)
                currentItemIndex = bagCapacity - 1;
        }
        float mw = Input.GetAxis("Mouse ScrollWheel");
        if (mw < 0.1)
        {
            currentItemIndex++;
            currentItemIndex %= bagCapacity;
        }
        if (mw > -0.1)
        {
            currentItemIndex--;
            if (currentItemIndex < 0)
                currentItemIndex = bagCapacity - 1;
        }
        UpdateInventory();
    }
    private void Update()
    {
        CurrentItemChanging();
    }
}