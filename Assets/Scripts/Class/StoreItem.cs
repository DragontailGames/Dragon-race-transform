using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoreItem<T>
{
    public string name;

    public int id;

    public int price;

    public T item;

    public bool buyed = false;
}
