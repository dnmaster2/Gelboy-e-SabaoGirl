using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Private Fields

    private GameObject _player;

    #endregion



    #region Public Fields

    public static int enemies;
    public GameObject endOfLevelPopup;
    public GameObject playerDeathPopup;
    public List<GameObject> activeIA;
    public int maxActiveIA;

    #endregion



    #region MonoBehaviour Callbakcs

    private void Start()
    {
        Time.timeScale = 1;
        if(SceneManager.GetActiveScene().name == "Fase1" || SceneManager.GetActiveScene().name == "Fase2" || SceneManager.GetActiveScene().name == "Fase3" || SceneManager.GetActiveScene().name == "Fase4" )
        {
          FindObjectOfType<AudioManager>().Play("Level");
        }

        else if(SceneManager.GetActiveScene().name == "Map")
        {
          FindObjectOfType<AudioManager>().Play("Map");
        }

        else if(SceneManager.GetActiveScene().name == "MainMenu")
        {
          FindObjectOfType<AudioManager>().Play("Main Menu");
        }

        else if (SceneManager.GetActiveScene().name == "FaseBoss")
        {
            FindObjectOfType<AudioManager>().Play("BossMusic");
        }

        else
        {
          FindObjectOfType<AudioManager>().Play("Load");
        }
    }

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        int p = PlayerPrefs.GetInt("player", 1);
        if (p == 1)
        {
            _player.GetComponent<TeleportSkill>().enabled = true;
            GameObject.FindGameObjectWithTag("Gel").SetActive(true);
            GameObject.FindGameObjectWithTag("Sab").SetActive(false);
        }
        else
        {
            _player.GetComponent<TeleportSkill>().enabled = false;
            _player.AddComponent<AreaDamageSkill>().areaSize = 4;
            GameObject.FindGameObjectWithTag("Gel").SetActive(false);
            GameObject.FindGameObjectWithTag("Sab").SetActive(true);
        }
    }

    #endregion



    #region Custom Callbacks

    public void EndLevel(int points)
    {
        int map = PlayerPrefs.GetInt("map");
        int level = PlayerPrefs.GetInt("level");
        if (map >= level)
        {
            //voltar ao mapa sem salvar progressão pois o level completo era anterior ao mais avançado
        }
        else
        {
            PlayerPrefs.SetInt("map", level);
        }
        Inventory.instance.SaveInvantory();
        GameObject p = Instantiate(endOfLevelPopup, GameObject.FindGameObjectWithTag("Canvas").transform);
        p.GetComponent<EndGamePopup>().SetupPopup(points, level);
    }

    public void PlayerDeath(int points)
    {
        GameObject d = Instantiate(playerDeathPopup, GameObject.FindGameObjectWithTag("Canvas").transform);
        d.GetComponent<PlayerDeathPopup>().SetupPopup(points);
    }

    public bool OnCameraList(GameObject enemyOnCamera)
    {
        bool alreadyOnList = activeIA.Contains(enemyOnCamera);
        if (!alreadyOnList && activeIA.Count < maxActiveIA)
        {
            activeIA.Add(enemyOnCamera);
            print("adcionado " + enemyOnCamera.name + " no Array");
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveFromCamera(GameObject objectToBeRemoved)
    {
        if (activeIA.Contains(objectToBeRemoved))
        {
            print("Removido " + objectToBeRemoved.name + " no Array");
            activeIA.Remove(objectToBeRemoved);
        }
    }

    #endregion



    #region Ienumerator Callbacks



    #endregion
}
