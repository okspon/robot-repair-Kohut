using UnityEngine;
using UnityEngine.InputSystem;
using Beginner2D;

public class PlayerController : MonoBehaviour
{
    [Header("Input Actions")]
    public InputAction MoveAction;
    public InputAction LaunchAction;
    public InputAction TalkAction;

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

    private Rigidbody2D rigidbody2d;
    private Animator animator;

    void Start()
    {
        MoveAction.Enable();
        LaunchAction.Enable();
        TalkAction.Enable();

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
    }

    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }

        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }

        if (LaunchAction.WasPressedThisFrame())
        {
            Launch();
        }

        if (TalkAction.WasPressedThisFrame())
        {
            RaycastHit2D hit = Physics2D.Raycast(
                rigidbody2d.position + Vector2.up * 0.2f,
                moveDirection,
                1.5f,
                LayerMask.GetMask("NPC")
            );

            if (hit.collider != null)
            {
                NonPlayerCharacter npc = hit.collider.GetComponent<NonPlayerCharacter>();
                if (npc != null)
                {
                    npc.DisplayDialogue();
                }
            }
        }
    }

    void FixedUpdate()
    {
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
        UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);

        if (currentHealth <= 0)
        {
            GameManager.instance.LoseGame();
        }
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(
            projectilePrefab,
            rigidbody2d.position + Vector2.up * 1.0f,
            Quaternion.identity
        );

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.Launch(moveDirection, 5f);
        }

        animator.SetTrigger("Launch");
    }
}