using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject configuracoes, menu;
    public AudioSource asMenu;
    public AudioClip acBtnClick;

    private void Start()
    {
        asMenu = GetComponent<AudioSource>();
    }

    public void ChangeScreen()
    {
        configuracoes.SetActive(!configuracoes.activeSelf);
        menu.SetActive(!menu.activeSelf);
    }


    public void PlayButton()
    {
        PlaySound();
        SceneManager.LoadScene(1);
    }

    void PlaySound()
    {
        asMenu.pitch = Random.Range(0.85f, 1.15f);
        asMenu.volume = .15f;
        asMenu.clip = acBtnClick;
        asMenu.Play();
    }
}