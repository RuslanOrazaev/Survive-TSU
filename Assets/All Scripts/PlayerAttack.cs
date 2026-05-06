using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public float damage = 25f;
    public Camera playerCamera;

    void Update()
    {
        // Атака
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        // Взаимодействие с дверью
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractWithDoor();
        }
    }

    void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, attackRange))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                EnemyAI enemy = hit.transform.GetComponent<EnemyAI>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }

    void InteractWithDoor()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, attackRange))
        {
            DoorController door = hit.transform.GetComponent<DoorController>();
            if (door != null)
            {
                door.ToggleDoor();
                return;
            }

            // Проверяем родителя (если попали в модель, а скрипт на петлях)
            if (hit.transform.parent != null)
            {
                door = hit.transform.parent.GetComponent<DoorController>();
                if (door != null)
                {
                    door.ToggleDoor();
                }
            }
        }
    }
}