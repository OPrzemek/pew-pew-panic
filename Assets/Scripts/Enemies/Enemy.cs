using Enums;
using Managers;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Rodzaj przeciwnika- red lub orage

    public EnemyColor enemyColor;

    //max i aktualne hp przeciwnika
    public int maxHealth;
    private int currentHealth;


    void Start()
    {
        maxHealth = EnemyManager.Instance.enemyHealth;
        currentHealth = maxHealth;//ustawienie akutalnego hp na max
    }

    private void Update()
    {
        Vector3 s = transform.parent.transform.localScale;
        transform.localScale = new Vector3(Mathf.Max(1f, 1f / s.x), Mathf.Max(1f, 1f / s.y), Mathf.Max(1f, 1f / s.z));
    }

    //metoda otrzymywania Damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }

    }
    void Die()
    { 
        //Dodanie punktów do GameManagera
        GameManager.Instance.AddPoints(maxHealth);

        Destroy(gameObject);//usunie obiekt z gry
    }
}
