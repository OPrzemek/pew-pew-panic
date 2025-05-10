using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int projectilesPerShot = 3;
    public float spreadAngle = 10f;
    public GameObject projectilePrefab;

    public void CustomUpdate()

    //Obracanie broni wokó³ statku
    {

    }
    //strzelanie
    public void Shoot()
    {
        for(int i = 0; i < projectilesPerShot; i++)//Powtórka tyle razy, ile pocisków chce sie wystrzeliæ
        {
            //Jesli jest wiecej ni¿ 1 pocisk, losuje siê rozrzut (prawo/lewo), jesli jest tylko 1 to nie ma rozrzutu
            float spread = (projectilesPerShot > 1) ? Random.Range(-spreadAngle, spreadAngle) : 0f;
            //dodanie pocisku
            GameObject projectile = Instantiate(projectilePrefab,transform);
            //nadanie predkosci liniowej cia³a fizycznemego pocisku
            Rigidbody2D rbProjectile= projectile.GetComponent<Rigidbody2D>();
            Debug.Log(gameObject.transform.forward);
            rbProjectile.linearVelocity= gameObject.transform.up*spread;
        }

    }
}
