using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{

    #region Public Fields

    [Tooltip("Que item é esse? apenas arraste o objeto item para cá!")]
    public Item itemType;
    [Tooltip("Popup explicativo do item")]
    public GameObject popup;

    #endregion

    public GameObject particleFX;

    #region MonoBehaviour Callbakcs

    private void Awake()
    {
        if (itemType == null)
        {
            Debug.LogError("Este objeto precisa de um tipo de item", this);
            GetComponent<BoxCollider>().enabled = false;
            return;
        }
        Instantiate(itemType.itemPrefab, transform.position, transform.rotation, transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        //checa se o que colidiu é um jogador para adicionar ao inventario
        if (other.CompareTag("Player"))
        {
            if (CheckIfPlayerHasItem(itemType))
            {
                GameObject p = Instantiate(popup, GameObject.FindGameObjectWithTag("Canvas").transform);
                p.GetComponent<ItemPopup>().SetupPopup(itemType);

                GameObject particle = Instantiate(particleFX, transform.position, Quaternion.identity);
                Destroy(particle, 3f);

                Destroy(gameObject);
            }
            else
            {
                Inventory.instance.AddItem(itemType);
                GameObject p = Instantiate(popup, GameObject.FindGameObjectWithTag("Canvas").transform);
                p.GetComponent<ItemPopup>().SetupPopup(itemType);

                GameObject particle = Instantiate(particleFX, transform.position, Quaternion.identity);
                Destroy(particle, 3f);

                Destroy(gameObject);
            }

            FindObjectOfType<AudioManager>().Play("Pick");
        }
    }

    bool CheckIfPlayerHasItem(Item pickedUpItem)
    {
        foreach (Item invItem in Inventory.instance.items)
        {
            if (invItem.name == pickedUpItem.name)
            {
                return true;
            }
        }
        return false;
    }

    #endregion

}
