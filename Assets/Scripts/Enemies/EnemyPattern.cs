using UnityEngine;
using Enums;
using Managers;

public class EnemyPattern : MonoBehaviour
{
    public PatternType patternType;

    public Transform target;    //Cel=gracz    ??
    public float moveSpeed = 1f;//Szybko�� poruszania sie patternu do gracza??    

    //Ustawinia Rectangle i Circle
    public float shrinkSpeed = 0.05f;    // Szybko�� mniejszania sie patternu
    public float rotationSpeed = 30f;   //Szyko�� kr�cania sie dla Circle


    //Ustawinia Bounce
    public float bounceSpeed = 2f;                    //pr�dko�� poruszania sie DvD-boxa
    public Vector2 boundsMin = new Vector2(-4.68f, -3.66f);   //Lewy dolny r�g ekranu
    public Vector2 boundsMax = new Vector2(4.68f, 3.66f);   //Prawy g�rny r�g ekranu

    private Vector2 bounceDirection;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (patternType == PatternType.Bounce)
        {
            bounceDirection = Random.insideUnitCircle.normalized;   //Losowy start
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameState != GameState.Playing)
            return;
        switch (patternType)
        {
            case PatternType.Rectangle:
                UpdateRectangle();
                break;
            case PatternType.Circle:
                UpdateCircle();
                break;
            case PatternType.Bounce:
                UpdateBounce();
                break;
        }
    }


    // RECTANGLE
    void UpdateRectangle()
    {
        // Zmniejszanie
        transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;

        // Ruch w stron� gracza
        if (target != null)
        {
            Vector2 dir = (target.position - transform.position).normalized;
            transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);
        }
    }

    //CIRCLE
    void UpdateCircle()
    {
        // Obracanie wok� �rodka
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Zmniejszanie
        transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;

        // Ruch w stron� gracza
        if (target != null)
        {
            Vector2 dir = (target.position - transform.position).normalized;
            transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);

        }
    }
    //BOUNCE
    void UpdateBounce()
    {
        //Ruszamy ca�y obiekt w aktualnym kierunku, z dana predkoscia, plynnie w czasie
        transform.position += (Vector3)(bounceDirection * bounceSpeed * Time.deltaTime);

        //Zapami�tuje pozycj�, by sprawdzi�, czy trzeba go odbi� od kraw�dzi
        Vector2 pos = transform.position;

        // Odbijanie si� od bok�w ekranu(nie wiem co tu sie dzieje ale chyba dzia�a:D
        if (pos.x < boundsMin.x || pos.x > boundsMax.x)
        {
            bounceDirection.x *= -1;
            transform.position = new Vector2(Mathf.Clamp(pos.x, boundsMin.x, boundsMax.x), pos.y);
        }

        if (pos.y < boundsMin.y || pos.y > boundsMax.y)
        {
            bounceDirection.y *= -1;
            transform.position = new Vector2(pos.x, Mathf.Clamp(pos.y, boundsMin.y, boundsMax.y));
        }
    }


}

