using UnityEngine;

[CreateAssetMenu(fileName = "Novo Item", menuName = "Inventario/Item")]
public class Item : ScriptableObject
{
    [Tooltip("Nome do item")]
    new public string name = "new item";    //Nome do item
    [Tooltip("2D Sprite do item")]
    public Sprite icone = null;             //Icone do item
    [Tooltip("Gameobject que será spawnado para gerar o visual 3D do item")]
    public GameObject itemPrefab;
    [Tooltip("Duração do buff")]
    public float duration;
    [Tooltip("Descrição do que esse item faz")]
    public string description;
    [Tooltip("Frase para conscientização")]
    public string frase;

    public virtual void Usar()
    {
        //fazer algo
        Debug.Log("Usando " + name);
    }
}
