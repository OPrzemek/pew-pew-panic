using System.Collections;
using Managers;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int projectilesPerShot = 3;
    public float spreadAngle = 10f;
    public GameObject projectilePrefab;
    public float cooldown = 1;
    public int speed = 3;
    public int damage = 1;
    public SpriteRenderer Renderer;
    public Transform firePoint;         //punkt z ktorego startuja pociski

    //Obracanie broni wokół statku
    public void CustomUpdate()

    {
        transform.RotateAround(Ship.Instance.transform.position, Vector3.forward, 50f * Time.deltaTime);
    }
    // strzelanie pociskami w pętli co określony czas
    public IEnumerator Shoot()
    {
        while (true)
        {
            if (GameManager.Instance.GameState == Enums.GameState.Playing)
            {
                for (int i = 0; i < projectilesPerShot; i++)
                {
                    float spread = (projectilesPerShot > 1) ? Random.Range(-spreadAngle, spreadAngle) : 0f;

                    // Obliczanie kieruneku względem firePoint
                    Vector2 direction = (firePoint.position - transform.position).normalized;

                    // Dodanie rozrzut do kierunku (obrót wokół Z)
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + spread;
                    Vector2 finalDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

                    // Stworzenie pocisku i nadanie mu prędkości
                    GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.Euler(0, 0, angle), GameManager.Instance.ProjectileHolder.transform);
                    //Ustawienie obrażeń pociskowi
                    Projectile p = projectile.GetComponent<Projectile>();
                    p.damage = damage;
                    Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                    rb.linearVelocity = finalDirection * speed;
                }
                yield return new WaitForSeconds(cooldown);
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
