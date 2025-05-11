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
