using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    Inventory inventario;
    InventorySlot[] slots;
    void Start()
    {
        inventario = Inventory.instance;
        inventario.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }
    
    void UpdateUI()
    {
        Debug.Log("Update UI");
        for(int i = 0; i < slots.Length; i++)
        {
            if(i < inventario.items.Count)
            {
                slots[i].AddItem(inventario.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
