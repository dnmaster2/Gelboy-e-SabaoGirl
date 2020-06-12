using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region instanciaInventario

    public static Inventory instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Mais de uma instancia, algo está muito errado");
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int espaco = 5;

    public List<Item> items = new List<Item>();

    public bool AddItem (Item item)
    {
        if(items.Count >= espaco)
        {
            Debug.Log("Sem espaco no inventario");
            return false;
        }
        items.Add(item);
        if(onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
        return true;
    }
    public void RemoveItem(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
    public void ClearInventory()
    {
        foreach (Item i in items)
        {
            RemoveItem(i);
        }
    }
}
