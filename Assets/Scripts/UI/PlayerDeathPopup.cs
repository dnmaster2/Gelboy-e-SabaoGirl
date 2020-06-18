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
        PlaySound();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void Map()
    {
        PlaySound();
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
        Destroy(gameObject);
    }

    public void Retry()
    {
        PlaySound();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Destroy(gameObject);
    }

    void PlaySound()
    {
        Inventory.instance.asInventory.pitch = Random.Range(0.85f, 1.15f);
        Inventory.instance.asInventory.volume = .15f;
        Inventory.instance.asInventory.clip = Inventory.instance.adButtonClick;
        Inventory.instance.asInventory.Play();
    }
}
