using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Store<T>
{
    public string name;

    public List<StoreItem<T>> storeItems;

    public List<StoreCanvasItem<T>> storeCanvasItems;

    public UnityAction<int> spendMoney;

    public ScrollRect storeScrollRect;

    public GameObject prefabStoreItem;

    public int currentMoney;

    public Store(ScrollRect storeScrollRect, GameObject prefabStoreItem)
    {
        this.storeScrollRect = storeScrollRect;
        this.prefabStoreItem = prefabStoreItem;
    }

    public List<StoreItem<T>> GetStoreItems()
    {
        return storeItems.FindAll(n => n.buyed == false);
    }

    public void BuyItem(StoreItem<T> storeItem)
    {
        storeItems.Find(n => n == storeItem).buyed = true;
    }

    public bool CanBuy(StoreItem<T> storeItem)
    {
        return currentMoney - storeItem.price >= 0;
    }

    public void CreateCanvas(int startMoney)
    {
        currentMoney = startMoney;
        storeCanvasItems = new List<StoreCanvasItem<T>>();
        foreach (var aux in GetStoreItems())
        {
            var newStoreObject = GameObject.Instantiate(prefabStoreItem, storeScrollRect.content.transform);
            newStoreObject.transform.GetComponent<Button>().onClick.AddListener(() => 
            { 
                BuyItem(aux); 
                spendMoney(aux.price);
                GameObject.DestroyImmediate(newStoreObject);
                storeCanvasItems.Remove(storeCanvasItems.Find(n => n.storeItem == aux));
                UpdateItems();
            });
            newStoreObject.transform.GetComponent<Button>().interactable = CanBuy(aux);
            newStoreObject.transform.Find("Icon").GetComponent<Image>().sprite = aux.icon;
            newStoreObject.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = aux.price.ToString();

            storeCanvasItems.Add(new StoreCanvasItem<T>()
            {
                storeItem = aux,
                gameObject = newStoreObject
            });
        }
    }

    public void UpdateItems()
    {
        foreach(var aux in storeCanvasItems)
        {
            aux.gameObject.transform.GetComponent<Button>().interactable = CanBuy(aux.storeItem);
        }
    }
}
