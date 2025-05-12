using Managers;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;             // Ile obrarzeń zadaje pocisk
    public float lifeTime = 3f;        // Czas po ktorym pocisk znika
    public float speed = 5f;
    private Rigidbody2D rb;
    [SerializeField]
    private Vector2 currentVelocity;
    private float timer = 0f;

    void Start()
    {
        // referencja do Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // Nadanie pociskowi prędkość
        currentVelocity = rb.linearVelocity;
    }

    private void Update()
    {
        if (GameManager.Instance.GameState != Enums.GameState.Playing)
            rb.linearVelocity = new Vector2(0f, 0f);
        else
        {
            timer += Time.deltaTime;
            // Automatyczne spawnowanie co X sekund
            if (timer >= lifeTime)
                Destroy(gameObject);
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.linearVelocity = currentVelocity;
            currentVelocity = rb.linearVelocity;
        }
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
