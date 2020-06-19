using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject configuracoes, menu;
    
    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("Main Menu");
    }

    public void ChangeScreen()
    {
        configuracoes.SetActive(!configuracoes.activeSelf);
        menu.SetActive(!menu.activeSelf);
    }
}
