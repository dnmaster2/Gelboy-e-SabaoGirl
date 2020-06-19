using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePlayerScript : MonoBehaviour
{
    public GameObject _playerInterface;
    public AudioClip acBtnClick;
    public AudioSource asCanvas;

    private void Awake()
    {
        asCanvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<AudioSource>();
        Debug.Log(asCanvas);

        int p = PlayerPrefs.GetInt("player", 0);
        if (p == 0)
        {
            _playerInterface.SetActive(!_playerInterface.activeInHierarchy);
        }
    }

    public void ChooseGelman()
    {
        PlayerPrefs.SetInt("player", 1);
        ClickSound();
        OpenClosePlayerInterface();
    }

    public void ChooseSabaogirl()
    {
        PlayerPrefs.SetInt("player", 2);
        ClickSound();
        OpenClosePlayerInterface();
    }

    public void OpenClosePlayerInterface()
    {
        ClickSound();
        _playerInterface.SetActive(!_playerInterface.activeInHierarchy);
    }

    public void Backmenu()
    {
        ClickSound();
        SceneManager.LoadScene(0);
    }

    public void ClickSound()
    {
        asCanvas.pitch = Random.Range(0.85f, 1.15f);
        asCanvas.volume = .15f;
        asCanvas.clip = acBtnClick;
        asCanvas.Play();
    }
}
