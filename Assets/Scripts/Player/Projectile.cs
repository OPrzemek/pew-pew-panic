using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;             // Ile obrarzeń zadaje pocisk
    public float lifeTime = 3f;        // Czas po ktorym pocisk znika
    public float speed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        // referencja do Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // Nadanie pociskowi prędkość
        rb.linearVelocity = transform.right * speed;

        // Automatycznie zniszczy pocisk po kilku sekundach
        Destroy(gameObject, lifeTime);
    }
    //Uderzenie pocisku
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Sprawdzenie czy pocisk trafi w przeciwnika
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null )
        {
            //Zadaj obrażenia
            enemy.TakeDamage(damage);
            damage -= Mathf.Min(enemy.maxHealth, damage);

            //Zniszcz pocisk
            if(damage <= 0)
                Destroy(gameObject);
        }
    }
}
