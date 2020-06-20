using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private AudioSource asBtnClick;
    public AudioClip acBtnClick;
    public GameObject configuracoes, menu;
    
    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("Main Menu");
        asBtnClick = GetComponent<AudioSource>();
    }

    public void ChangeScreen()
    {
        PlayBtnClickSound();
        configuracoes.SetActive(!configuracoes.activeSelf);
        menu.SetActive(!menu.activeSelf);
    }

    public void StartButton()
    {
        PlayBtnClickSound();
        SceneManager.LoadScene(1);
    }

    public void CloseApp()
    {
        PlayBtnClickSound();
        Application.Quit();
    }

    public void PlayBtnClickSound()
    {
        asBtnClick.pitch = Random.Range(0.85f, 1.15f);
        asBtnClick.volume = .15f;
        asBtnClick.clip = acBtnClick;
        asBtnClick.Play();
    }
}
