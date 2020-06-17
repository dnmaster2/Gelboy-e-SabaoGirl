using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGamePopup : MonoBehaviour
{
    [Tooltip("Tempo que irá demorar para inflar esta ui")]
    public float popupTime;
    public Text completeMassage, pointsText;

    private void Awake()
    {
        LeanTween.scale(this.gameObject, new Vector3(1, 1, 1), popupTime).setOnComplete(PauseTime);
    }

    public void SetupPopup(int points, int map)
    {
        pointsText.text = points.ToString();
        completeMassage.text = $"Voce completou o nivel {map}";
    }

    void PauseTime()
    {
        Time.timeScale = 0;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Map()
    {
        SceneManager.LoadScene(2);
    }

    public void Next()
    {
        int l = PlayerPrefs.GetInt("level", 0);
        if (l >= 5)
        {
            Map();
        }
        else
        {
            PlayerPrefs.SetInt("level", l + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
