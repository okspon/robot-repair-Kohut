using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force, ForceMode2D.Impulse);
        Debug.Log("Launch викликано");
    }

    private void Update()
    {
        // Видаляємо снаряд, якщо він занадто далеко відлетів
        if (transform.position.magnitude > 100.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Шукаємо робота під час зіткнення
        EnemyController enemy = other.GetComponent<EnemyController>();

        if (enemy != null)
        {
            enemy.Fix(); // Викликаємо метод ремонту!
        }

        // Знищуємо снаряд при влучанні
        Destroy(gameObject);
    }
}