using Enums;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Rodzaj przeciwnika- red lub orage

    public EnemyColor enemyColor;

    //max i aktualne hp przeciwnika
    public int maxHealth = 5;
    private int currentHealth;


    void Start()
    {
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
        Destroy(gameObject);//usunie obiekt z gry
    }
}
