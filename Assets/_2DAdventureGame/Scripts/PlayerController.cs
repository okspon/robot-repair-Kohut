using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Input Actions")]
    public InputAction MoveAction;
    public InputAction LaunchAction;

    [Header("Movement Settings")]
    public float speed = 3.0f;
    private Vector2 move;
    private Vector2 moveDirection = new Vector2(1, 0);

    [Header("Health & Combat")]
    public int maxHealth = 5;
    public float timeInvincible = 2.0f;
    public GameObject projectilePrefab;

    public int health { get { return currentHealth; } }
    private int currentHealth;
    private bool isInvincible;
    private float damageCooldown;

    // Components
    private Rigidbody2D rigidbody2d;
    private Animator animator;

    void Start()
    {
        MoveAction.Enable();
        LaunchAction.Enable();

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
    }

    void Update()
    {
        // Зчитування руху
        move = MoveAction.ReadValue<Vector2>();

        // Оновлення напрямку погляду (Look Direction)
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }

        // Передача параметрів в Аніматор
        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        // Таймер безсмертя після отримання шкоди
        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }

        // Запуск снаряда
        if (LaunchAction.WasPressedThisFrame())
        {
            Launch();
        }
    }

    void FixedUpdate()
    {
        // Фізичне переміщення
        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            damageCooldown = timeInvincible;
            animator.SetTrigger("Hit");
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    void Launch()
    {
        // Створення снаряда із зсувом по висоті
        GameObject projectileObject = Instantiate(
            projectilePrefab,
            rigidbody2d.position + Vector2.up * 1.0f,
            Quaternion.identity
        );

        // Запуск снаряда у напрямку погляду персонажа
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.Launch(moveDirection, 5f);
            Debug.Log("Напрямок: " + moveDirection);
        }

        animator.SetTrigger("Launch");
    }
}