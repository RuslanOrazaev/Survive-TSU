using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public float health = 100f;
    public Slider healthSlider;
    private bool isDead = false;

    private Animator animator;

    void Start()
    {
        if (healthSlider != null)
            healthSlider.value = health;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;
        if (healthSlider != null)
            healthSlider.value = health;
        Debug.Log("Получен урон: " + damage + ". Осталось HP: " + health);
    }

    void Die()
    {
        isDead = true;
        animator.SetBool("IsDead", isDead);
        animator.SetLayerWeight(1, 0f);
        Debug.Log("ГЕРОЙ ПОГИБ!");

        // Отключаем управление
        GetComponent<SimpleFPSControllers>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;

        // Замораживаем на месте
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
        }

        // Наклоняем персонажа — имитация падения
        transform.rotation = Quaternion.Euler(90f, transform.rotation.eulerAngles.y, 0f);

        // Перезагружаем сцену через 3 секунды
        Invoke("RestartScene", 6f);
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}