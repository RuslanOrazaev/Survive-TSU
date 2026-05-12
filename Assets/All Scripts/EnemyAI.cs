using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    public float stopDistance = 1.5f;

    [Header("Combat")]
    public float damage = 10f;
    public float attackCooldown = 3.2f;

    [Header("Stats")]
    public float health = 100f;

    private Transform player;
    private NavMeshAgent agent;

    Animator animator;

    public int weaponType = 0;
    private float lastAttackTime;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();

        agent.stoppingDistance = stopDistance;

        animator = GetComponent<Animator>();

        //начальное оружие для зомби - укусы
        animator.SetInteger("WeaponType", weaponType);
    }

    void Update()
    {
        if (player == null)
            return;

        // AI PATHFINDING
        agent.SetDestination(player.position);

        float distance =
            Vector3.Distance(transform.position, player.position);

        // Поворот к игроку
        Vector3 lookPos = player.position - transform.position;
        lookPos.y = 0;

        if (lookPos != Vector3.zero)
        {
            Quaternion rot =
                Quaternion.LookRotation(lookPos);

            transform.rotation =
                Quaternion.Slerp(
                    transform.rotation,
                    rot,
                    Time.deltaTime * 8f
                );
        }

        // Attack
        if (distance <= stopDistance + 0.3f)
        {
            TryAttack();
        }
        UpdateAnimator();
    }
    void UpdateAnimator()
    {
        if (animator == null)
            return;
        Vector3 velocity = agent.velocity;
        Vector3 horizontalVel = new Vector3(velocity.x, 0, velocity.z);
        float speed = horizontalVel.magnitude;

        animator.SetFloat("Speed", speed);
        animator.SetFloat("VelX", horizontalVel.x);
        animator.SetFloat("VelY", horizontalVel.z);
        animator.SetFloat("Speed", horizontalVel.magnitude);
    }

    void TryAttack()
    {
        if (Time.time - lastAttackTime < attackCooldown)
            return;
        animator.SetTrigger("Attack");
        PlayerStats stats =
            player.GetComponent<PlayerStats>();

        if (stats != null)
        {
            stats.TakeDamage(damage);

            Debug.Log("ВРАГ БЬЁТ! Урон: " + damage);

            lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;

        Debug.Log(
            "Враг получил урон: " +
            dmg +
            ". HP врага: " +
            health
        );

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("ВРАГ УБИТ!");

        agent.enabled = false;

        Destroy(gameObject);
    }
}