using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject configuracoes, menu;
    public void ChangeScreen()
    {
        configuracoes.SetActive(!configuracoes.activeSelf);
        menu.SetActive(!menu.activeSelf);
    }
}