using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int projectilesPerShot = 3;
    public float spreadAngle = 10f;
    public GameObject projectilePrefab;

    public void CustomUpdate()

    //Obracanie broni wok� statku
    {

    }
    //strzelanie
    public void Shoot()
    {
        for(int i = 0; i < projectilesPerShot; i++)//Powt�rka tyle razy, ile pocisk�w chce sie wystrzeli�
        {
            //Jesli jest wiecej ni� 1 pocisk, losuje si� rozrzut (prawo/lewo), jesli jest tylko 1 to nie ma rozrzutu
            float spread = (projectilesPerShot > 1) ? Random.Range(-spreadAngle, spreadAngle) : 0f;
            //dodanie pocisku
            GameObject projectile = Instantiate(projectilePrefab,transform);
            //nadanie predkosci liniowej cia�a fizycznemego pocisku
            Rigidbody2D rbProjectile= projectile.GetComponent<Rigidbody2D>();
            Debug.Log(gameObject.transform.forward);
            rbProjectile.linearVelocity= gameObject.transform.up*spread;
        }

    }
}
