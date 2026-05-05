using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    public float stopDistance = 1.5f;  // на каком расстоянии останавливается
    public float damage = 10f;
    public float attackCooldown = 1f;
    public float health = 100f;

    private Transform player;
    private float lastAttackTime = 0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        // Смотрим на игрока
        Vector3 lookPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(lookPos);

        float distance = Vector3.Distance(transform.position, player.position);

        // Двигаемся, только если далеко
        if (distance > stopDistance)
        {
            Vector3 moveDir = (player.position - transform.position).normalized;
            moveDir.y = 0; // не летаем
            transform.position += moveDir * speed * Time.deltaTime;
        }

        // Атакуем, если близко
        if (distance <= stopDistance + 0.5f)
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                PlayerStats stats = player.GetComponent<PlayerStats>();
                if (stats != null)
                {
                    stats.TakeDamage(damage);
                    Debug.Log("ВРАГ БЬЁТ! Урон: " + damage);
                    lastAttackTime = Time.time;
                }
            }
        }
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        Debug.Log("Враг получил урон: " + dmg + ". HP врага: " + health);

        if (health <= 0)
        {
            Debug.Log("ВРАГ УБИТ!");
            Destroy(gameObject);
        }
    }
}