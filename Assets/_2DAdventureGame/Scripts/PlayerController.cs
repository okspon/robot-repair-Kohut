using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Поле для створення дії вводу
    public InputAction MoveAction;

    // Швидкість руху персонажа
    public float speed = 3.0f;

    // Оголошуємо змінні для фізики та напрямку
    Rigidbody2D rigidbody2d;
    Vector2 move;

    void Start()
    {
        // Обов'язково вмикаємо дію вводу при старті гри
        MoveAction.Enable();

        // Отримуємо компонент Rigidbody2D з персонажа
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Зчитуємо напрямок вводу щокадру
        move = MoveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        // Розраховуємо нову позицію з урахуванням швидкості та кроку фізики
        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;

        // Рухаємо Rigidbody2D через фізичний рушій (щоб уникнути тремтіння біля стін)
        rigidbody2d.MovePosition(position);
    }
}