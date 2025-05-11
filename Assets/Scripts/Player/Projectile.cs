using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;             // Ile obra�e� zadaje pocisk
    public float lifeTime = 3f;        // Czas po kt�rym pocisk znika
    public float speed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        // referencja do Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // Nadanir pociskowi pr�dko��
        rb.linearVelocity = transform.right * speed;

        // Automatycznie zniszcz pocisk po kilku sekundach
        Destroy(gameObject, lifeTime);
    }
    //Uderzenie pocisku w co�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Sprawdzenie czy pocisk trafi� w przeciwnika
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null )
        {
            //Zadaj obra�enia
            enemy.TakeDamage(damage);

            //Zniszcz pocisk
            Destroy(gameObject);
        }
    }
}
