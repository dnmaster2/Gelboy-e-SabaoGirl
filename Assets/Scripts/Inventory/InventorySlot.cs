using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventorySlot : MonoBehaviour
{
    public Image icone;
    private Item item;
    public void AddItem(Item newItem)
    {
        item = newItem;
        icone.sprite = item.icone;
        icone.enabled = true;
    }
    public void ClearSlot()
    {
        item = null;
        icone.sprite = null;
        icone.enabled = false;
    }
    public void UsarItem()
    {
        if(item != null)
        {
            item.Usar();
            switch (item.name)
            {
                case "Mascara":
                    BuffManager.instance.Mask();
                    Inventory.instance.RemoveItem(item);
                    break;
                case "Canhao":
                    BuffManager.instance.Cannon();
                    break;
                case "Luva":
                    StartCoroutine(BuffManager.instance.DamageBuff(item.duration));
                    Inventory.instance.RemoveItem(item);
                    break;
                case "Respawn":
                    BuffManager.instance.ActivateRespawn();
                    Inventory.instance.RemoveItem(item);
                    break;
                default:
                    Debug.LogError("Nome inválido para item", item);
                    break;
            }
        }
    }
}
