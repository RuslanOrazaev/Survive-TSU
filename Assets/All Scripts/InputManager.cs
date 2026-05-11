
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    private PlayerInputs playerInput;
    private PlayerInputs.OnFootActions onFoot;


    [SerializeField] private SimpleFPSControllers motor;
    [SerializeField] private PlayerAttack playerAttack;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInputs();
        onFoot = playerInput.OnFoot;
        if (motor == null)
            motor = GetComponent<SimpleFPSControllers>();
        if (playerAttack == null)
            playerAttack = GetComponent<PlayerAttack>();
        if (playerAttack == null)
            Debug.LogError("❌ InputManager: не найден PlayerAttack на объекте!");
        if (motor == null)
            Debug.LogError("❌ InputManager: не найден Motor на объекте!");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = onFoot.Move.ReadValue<Vector2>();
        Vector2 lookInput = onFoot.Look.ReadValue<Vector2>();

        // Считываем нажатия кнопок (замени Jump и Attack на точные имена из твоего ассета)
        bool jumpPressed = onFoot.Jump.triggered;
        bool attackPressed = onFoot.Attack.triggered;
        bool interactPressed = onFoot.Interact.triggered;

        // ПЕРЕДАЕМ все данные в мотор (этой строки у тебя не было)
        motor.SetInput(moveInput, lookInput, jumpPressed);

        if (attackPressed)
            playerAttack.Attack();

        if (interactPressed)
            playerAttack.InteractWithDoor();
    }
    private void OnEnable()
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
    }
}
