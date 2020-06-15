using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Private Fields



    #endregion



    #region Public Fields



    #endregion



    #region MonoBehaviour Callbakcs



    #endregion



    #region Custom Callbacks

    public void EndLevel()
    {
        int map = PlayerPrefs.GetInt("map");
        int level = PlayerPrefs.GetInt("level");
        if (map >= level)
        {
            //voltar ao mapa sem salvar progressão pois o level completo era anterior ao mais avançado
            return;
        }
        else
        {
            PlayerPrefs.SetInt("map", level);
        }
    }

    #endregion



    #region Ienumerator Callbacks



    #endregion
}
