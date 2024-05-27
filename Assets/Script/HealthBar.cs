using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    private float health;
    public float maxHealth = 100f;
    [SerializeField]
    private TextMeshProUGUI healthText;
    [SerializeField]
    private TextMeshProUGUI playerStatus;
    public Image leftHealthBar;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        if(health==0) playerStatus.text = "You Died!";
    }
    public void UpdateHealthUI()
    {
        healthText.text = $"HP : {health}/{maxHealth}";
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        ChangeWidth(leftHealthBar.rectTransform, health);
    }
    void ChangeWidth(RectTransform rectTransform, float newWidth)
    {
        float relWidth = (newWidth/100)*300;
        rectTransform.sizeDelta = new Vector2(relWidth, rectTransform.sizeDelta.y);
    }
}
