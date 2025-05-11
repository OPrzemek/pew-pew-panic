using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public static Ship Instance;
    //USTAWIENIA STATKU
    public int maxHealth = 100;//MAx zdrowie
    public int currentHealth;//atualne zdrowie 
    public GameObject weaponPrefab;
    //LISTA BRONI
    public List<Weapon> weapons = new List<Weapon>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }

    //METODA INICJALIZACJI
    public void Initialize()
    {
        //Ustawienie zrodiwa na max przy starcie gry
        currentHealth=maxHealth;
        //dodanie startowej broni
        Weapon weapon = Instantiate(weaponPrefab,transform).GetComponent<Weapon>();
        AddWeapon(weapon);
        StartCoroutine(weapon.Shoot());
    }

    //FUNKCJA UPDATE DLA STATKU
    public void CustomUpdate()
    {
        /*foreach (var weapon in weapons)
        {
            StartCoroutine(weapon.Shoot());
        }*/
    }
    private void Update()
    {
        float rotateInput = Input.GetAxis("Horizontal"); // A/D albo strza³ki
        transform.Rotate(Vector3.forward, -rotateInput * 100f * Time.deltaTime);

        if (Input.GetMouseButton(0)) // lewy przycisk myszy
        {
            CustomUpdate();
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
