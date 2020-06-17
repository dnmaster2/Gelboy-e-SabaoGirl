using UnityEngine;
using UnityEngine.UI;

public class ItemPopup : MonoBehaviour
{
    public float popupTime, popoutTime;
    public Image itemImage;
    public Text descriptionText, fraseText;

    private void Awake()
    {
        LeanTween.scale(this.gameObject, new Vector3(1, 1, 1), popupTime).setOnComplete(PauseTime);
    }

    public void SetupPopup(Item i)
    {
        itemImage.sprite = i.icone;
        descriptionText.text = i.description;
        fraseText.text = i.frase;
    }

    void PauseTime()
    {
        Time.timeScale = 0;
    }

    public void ClosePopup()
    {
        LeanTween.scale(this.gameObject, Vector3.zero, popoutTime).setDestroyOnComplete(true);
        Time.timeScale = 1;
    }
}
