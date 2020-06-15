using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapScript : MonoBehaviour
{
    [Tooltip("Número do mapa, usado para carregar este mapa caso o jogador tenha terminado o mapa anterior")]
    public int mapNumber = 1;

    public void GoToMap()
    {
        int m = PlayerPrefs.GetInt("map", 0);
        Debug.Log($"Last completed map is {m}");
        if (mapNumber <= m+1)
        {
            Debug.Log($"Loading map {mapNumber}");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + mapNumber);
            PlayerPrefs.SetInt("level", mapNumber);
        }
    }
}
