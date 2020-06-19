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
        PlaySound();
        SceneManager.LoadScene(0);
    }

    public void Map()
    {
        PlaySound();
        SceneManager.LoadScene(1);
    }
    
    public void Next()
    {
        PlaySound();
        int l = PlayerPrefs.GetInt("level", 0);
        if (l >= 5)
        {
            Map();
        }
        else
        {
            int next = l + 1;
            PlayerPrefs.SetInt("level", next);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void PlaySound()
    {
        Inventory.instance.asInventory.pitch = Random.Range(0.85f, 1.15f);
        Inventory.instance.asInventory.volume = .15f;
        Inventory.instance.asInventory.clip = Inventory.instance.adButtonClick;
        Inventory.instance.asInventory.Play();
    }
}
