using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Поле для створення дії вводу
    public InputAction MoveAction;

    // Швидкість руху персонажа
    public float speed = 3.0f;

    void Start()
    {
        // Обов'язково вмикаємо дію вводу при старті гри
        MoveAction.Enable();
    }

    void Update()
    {
        // Зчитуємо напрямок (Vector2: X та Y) від натиснутих клавіш
        Vector2 move = MoveAction.ReadValue<Vector2>();

        // Отримуємо поточну позицію
        Vector2 position = transform.position;

        // Додаємо зміщення з урахуванням швидкості та Time.deltaTime (плавний рух при будь-якому FPS)
        position.x = position.x + speed * move.x * Time.deltaTime;
        position.y = position.y + speed * move.y * Time.deltaTime;

        // Застосовуємо нову позицію
        transform.position = position;
    }
}

