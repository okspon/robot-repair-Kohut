using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Видалення снаряда, якщо він вилетів занадто далеко від центру карти
        if (transform.position.magnitude > 100.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        Debug.Log("Launch викликано");

        rigidbody2d.linearVelocity = direction.normalized * 5f;

        Debug.Log("Швидкість монети: " + rigidbody2d.linearVelocity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Перевірка на влучання у ворога
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.Fix();
        }

        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Видалення при зіткненні з іншими фізичними об'єктами
        Destroy(gameObject);
    }
}