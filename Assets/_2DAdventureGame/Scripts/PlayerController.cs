using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Поле для створення дії вводу
    public InputAction MoveAction;

    // Швидкість руху персонажа
    public float speed = 3.0f;
    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    int currentHealth;

    //систему невразливості
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float damageCooldown;
    // Оголошуємо змінні для фізики та напрямку
    Rigidbody2D rigidbody2d;
    Vector2 move;

    void Start()
    {
        // Обов'язково вмикаємо дію вводу при старті гри
        MoveAction.Enable();

        // Отримуємо компонент Rigidbody2D з персонажа
        rigidbody2d = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
    }

    void Update()
    {
        // Зчитуємо напрямок вводу щокадру
        move = MoveAction.ReadValue<Vector2>();

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
                isInvincible = false;
        }
    }

    void FixedUpdate()
    {
        // Розраховуємо нову позицію з урахуванням швидкості та кроку фізики
        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;

        // Рухаємо Rigidbody2D через фізичний рушій (щоб уникнути тремтіння біля стін)
        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible) return;
            isInvincible = true;
            damageCooldown = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}