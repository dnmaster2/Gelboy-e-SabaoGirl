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
    private int usedInventory;
    public List<Item> loadItem = new List<Item>();

    public AudioClip adButtonClick;
    public AudioSource asInventory;

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

    private void Start()
    {
        LoadInventory();
        asInventory = GetComponent<AudioSource>();
    }

    public void SaveInvantory()
    {
        //Deleta inventario salvado anteriormente
        DeleteInventory();
        for (int i = 0; i < items.Count; i++)
        {
            //salva cada item do inventario
            PlayerPrefs.SetString($"inventory{i}", items[i].name);
            Debug.Log($"Saved {items[i].name} item in: inventory{i}");
        }
        //salva a quantidade de itens
        usedInventory = items.Count;
        PlayerPrefs.SetInt("usedInventory", usedInventory);
        Debug.Log($"Saved {usedInventory} slots used");
    }

    public void LoadInventory()
    {
        //pega a quantidade de itens anteriormente salvada
        usedInventory = PlayerPrefs.GetInt("usedInventory");
        Debug.Log(usedInventory);
        //cria uma lista para ser preenchida com cada item salvado
        List<Item> loadedItems = new List<Item>();
        Debug.Log("Lista criada");
        //cria um tem base para preencher a lista
        Item load = loadItem[0];
        for (int i = 0; i < usedInventory; i++)
        {
            //procura o item salvado nesta posição
            string itemName = PlayerPrefs.GetString($"inventory{i}");
            Debug.Log($"Item {itemName} encontrado");
            for (int j = 0; j < loadItem.Count; j++)
            {
                if (loadItem[j].name == itemName)
                {
                    //procura um item na lista de itens possiveis de se obter
                    Debug.Log("Item " + itemName + " encontrado!");
                    //atribui ao item base
                    load = loadItem[j];
                }
            }
            //adiciona o item
            loadedItems.Add(load);
        }
        //preenche o inventario com a lista criada
        Debug.Log("Preenchendo Inventario");
        FillInventory(loadedItems);
    }

    private void FillInventory(List<Item> li)
    {
        foreach (Item i in li)
        {
            //usa a lista enviada para preencher o inventario
            AddItem(i);
        }
    }

    public void ClearInventory()
    {
        for (int i = items.Count-1; i > -1; i--)
        {
            RemoveItem(items[i]);
        }
    }

    public void DeleteInventory()
    {
        int i = PlayerPrefs.GetInt("usedInventory");
        for (int j = 0; i < usedInventory; i++)
        {
            PlayerPrefs.DeleteKey($"inventory{j}");
        }
        PlayerPrefs.DeleteKey("usedInventory");
    }
}
