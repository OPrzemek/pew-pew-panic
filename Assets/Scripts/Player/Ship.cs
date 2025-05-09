using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    //USTAWIENIA STATKU
    public int maxHealth = 100;//MAx zdrowie
    public int currentHealth;//atualne zdrowie 
    public GameObject weaponPrefab;
    //LISTA BRONI
    public List<Weapon> weapons = new List<Weapon>();

    //METODA INICJALIZACJI
    public void Initialize()
    {
        //Ustawienie zrodiwa na max przy starcie gry
        currentHealth=maxHealth;
        //dodanie startowej broni
        Weapon weapon = Instantiate(weaponPrefab).GetComponent<Weapon>();
        AddWeapon(weapon); 
    }

    //FUNKCJA UPDATE DLA STATKU
    public void CustomUpdate()
    {
        foreach (var weapon in weapons)
        {
            weapon.Shoot();
        }
    }

    //DODANIE NOWEJ BRONI
    public void AddWeapon(Weapon newWeapon)
    {
        weapons.Add(newWeapon);//dodanie broni do listy
       
    }

    //"DOSTANIE" OBRAZEÑ
    public void TakeDamage(int damage)
    {
        currentHealth-=damage;//Odejmowanie obra¿eñ od zdrowia
        if (currentHealth == 0)
        {
            Die();
        }
    }

   

    //ZNISZCZENIE STATKU
    public void Die()
    {

    }
}
