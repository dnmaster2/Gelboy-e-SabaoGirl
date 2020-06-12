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
                    break;
                case "Canhao":
                    break;
                case "Luva":
                    StartCoroutine(BuffManager.instance.DamageBuff(item.duration));
                    break;
                case "Respawn":
                    BuffManager.instance.ActivateRespawn();
                    break;
                default:
                    Debug.LogError("Nome inválido para item", item);
                    break;
            }
            Inventory.instance.RemoveItem(item);
        }
    }
}
