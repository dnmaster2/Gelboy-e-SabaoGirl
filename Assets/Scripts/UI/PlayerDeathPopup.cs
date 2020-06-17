using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDeathPopup : MonoBehaviour
{
    [Tooltip("Tempo que irá demorar para inflar esta ui")]
    public float popupTime;
    public Text deathMessage, pointsText;
    public string[] deathMessages = new string[0];

    private void Awake()
    {
        LeanTween.scale(this.gameObject, new Vector3(1, 1, 1), popupTime).setOnComplete(PauseTime);
    }

    public void SetupPopup(int points)
    {
        pointsText.text = points.ToString();
        deathMessage.text = deathMessages[Random.Range(0, deathMessages.Length)];
    }

    void PauseTime()
    {
        Time.timeScale = 0;
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void Map()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
        Destroy(gameObject);
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Destroy(gameObject);
    }
}
