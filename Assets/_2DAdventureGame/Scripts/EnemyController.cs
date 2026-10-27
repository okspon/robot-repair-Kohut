using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1.0f;
    public bool vertical;
    public float changeTime = 3.0f;

    Rigidbody2D rigidbody2d;
    Animator animator;
    float timer;
    int direction = 1;

    // ѕрапорець стану ворога (поламаний чи в≥дремонтований)
    bool broken = true;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = changeTime;
    }

    void Update()
    {
        // якщо ворог уже в≥дремонтований Ч не оновлюЇмо таймер ≥ рух
        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        // якщо ворог в≥дремонтований Ч в≥н зупин€Їтьс€ ≥ не рухаЇтьс€
        if (!broken)
        {
            return;
        }

        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            position.y = position.y + speed * direction * Time.deltaTime;

            if (animator != null)
            {
                animator.SetFloat("Move X", 0);
                animator.SetFloat("Move Y", direction);
            }
        }
        else
        {
            position.x = position.x + speed * direction * Time.deltaTime;

            if (animator != null)
            {
                animator.SetFloat("Move X", direction);
                animator.SetFloat("Move Y", 0);
            }
        }

        rigidbody2d.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ¬≥дремонтований ворог не дамажить гравц€
        if (!broken) return;

        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    // ѕубл≥чний метод ремонту, €кий викликаЇ Projectile.cs при влучанн≥
    public void Fix()
    {
        broken = false;

        if (rigidbody2d != null)
        {
            rigidbody2d.simulated = false; // ¬имикаЇмо ф≥зику
        }

        if (animator != null)
        {
            animator.SetTrigger("Fixed"); // «апускаЇмо тригер "Fixed"
        }
    }
}