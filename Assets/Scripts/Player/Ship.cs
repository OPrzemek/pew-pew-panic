using Managers;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public static Ship Instance;
    //USTAWIENIA STATKU
    public int maxHealth = 10;//MAx zdrowie
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

    private void Update()
    {
        float rotateInput = Input.GetAxis("Horizontal"); // A/D albo strza³ki
        transform.Rotate(Vector3.forward, -rotateInput * 100f * Time.deltaTime);
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
        UIManager.Instance.UpdateHPSlider();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy != null)
        {
            TakeDamage(1);
            Destroy(enemy.gameObject);
        }
    }

    //ZNISZCZENIE STATKU
    public void Die()
    {
        GameManager gM = GameManager.Instance;
        gM.GamePanel.SetActive(false);
        gM.GameState = Enums.GameState.End;
        gM.EndGamePanel.SetActive(true);
        gM.EndGameText.text = $"LVL: {gM.Level}\r\nPOINTS: {gM.Points}\r\nTIME: I don't know";
        Destroy(gameObject);
    }
}
