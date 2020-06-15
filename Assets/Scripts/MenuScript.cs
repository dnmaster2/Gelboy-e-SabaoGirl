using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject configuracoes, menu;
    public void ChangeScreen()
    {
        configuracoes.SetActive(!configuracoes.activeSelf);
        menu.SetActive(!menu.activeSelf);
    }


    public void PlayButton()
    {
        int p = PlayerPrefs.GetInt("player", 0);
        if (p != 0)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}