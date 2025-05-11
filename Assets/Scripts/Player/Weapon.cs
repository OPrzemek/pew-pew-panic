using Managers;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int projectilesPerShot = 3;
    public float spreadAngle = 10f;
    public GameObject projectilePrefab;
    public int cooldown = 1;
    public int speed = 3;
    public int damage = 5;
    public bool shooting = false;

    private async void Start()
    {
        shooting = true;
        await Shoot();
    }

    public void CustomUpdate()

    //Obracanie broni wok� statku
    {
        transform.RotateAround(Ship.Instance.transform.position, Vector3.forward, 50f * Time.deltaTime);
    }
    //strzelanie
    public async Awaitable Shoot()
    {
        while(shooting)
        {
            for (int i = 0; i < projectilesPerShot; i++)//Powt�rka tyle razy, ile pocisk�w chce sie wystrzeli�
            {
                //Jesli jest wiecej ni� 1 pocisk, losuje si� rozrzut (prawo/lewo), jesli jest tylko 1 to nie ma rozrzutu
                float spread = (projectilesPerShot > 1) ? Random.Range(-spreadAngle, spreadAngle) : 0f;
                //dodanie pocisku
                GameObject projectile = Instantiate(projectilePrefab, transform);
                //nadanie predkosci liniowej cia�a fizycznemego pocisku
                Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
                rbProjectile.linearVelocity = gameObject.transform.up * spread;
            }
            await Awaitable.WaitForSecondsAsync(cooldown);
        }
    }
}
