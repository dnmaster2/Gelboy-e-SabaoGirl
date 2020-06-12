using UnityEngine;

[CreateAssetMenu(fileName = "Novo Item", menuName = "Inventario/Item")]
public class Item : ScriptableObject
{
    new public string name = "new item";    //Nome do item
    public Sprite icone = null;             //Icone do item
    public GameObject itemPrefab;
    public float duration;

    public virtual void Usar()
    {
        //fazer algo
        Debug.Log("Usando " + name);
    }
}
