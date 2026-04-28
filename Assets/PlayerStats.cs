using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float health = 100f;
    public Slider healthSlider;

    void Start()
    {
        if (healthSlider != null)
            healthSlider.value = health;
    }

    void Update()
    {
        if (health <= 0)
        {
            Debug.Log("Герой погиб");
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (healthSlider != null)
            healthSlider.value = health;
        Debug.Log("Получен урон: " + damage + ". Осталось HP: " + health);
    }
}