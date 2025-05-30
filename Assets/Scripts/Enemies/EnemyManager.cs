using Enums;
using Managers;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    //Ustawienia spawnowania
    public GameObject[] patternPrefabs;      // Prefaby wzorc�w (Rectangle, Circle, Bounce)
    public Transform playerTransform;        // Gracz � cel dla pattern�w
    public float spawnInterval = 15f;         // Co ile sekund spawnowa� pattern
    public int enemyHealth = 1;

    private float timer = 0f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }
    public void Initialize()
    {

    }
    public void CustomUpdate()
    {
        if (GameManager.Instance.GameState != GameState.Playing)
            return;
        timer += Time.deltaTime;
        // Automatyczne spawnowanie co X sekund
        if (timer >= spawnInterval)
        {
            SpawnRandomPattern();//// Spawnuj now� fal�
            timer = 0f;
        }
        // Klawisz testowy: naci�nij P, �eby natychmiast zespawnowa� pattern
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnRandomPattern();
        }
    }

    void SpawnRandomPattern()
    {
        // Wybiera losowy wzorzec z listy prefab�w
        int index = Random.Range(0, patternPrefabs.Length);
        GameObject selectedPrefab = patternPrefabs[index];

        // Ustaw pozycj� pojawienia si� (je�eli nie jest to odbijaj�cy si� pattern, podaj pozycj� gracza)
        Vector2 spawnPos = Ship.Instance.transform.position;
        if(index == patternPrefabs.Length - 1)
            spawnPos = GetRandomSpawnPosition();

        // Stw�rz instancj� wzorca
        GameObject instance = Instantiate(selectedPrefab, spawnPos, Quaternion.identity, GameManager.Instance.EnemyHolder.transform);

        // Znajd� EnemyPattern na tym obiekcie
        EnemyPattern patternScript = instance.GetComponent<EnemyPattern>();
        if (patternScript != null)
        {
            patternScript.target = playerTransform; // Przeka� gracza jako cel
        }

    }
    Vector2 GetRandomSpawnPosition()
    {
        float x, y;
        int edge = Random.Range(0, 4); // 0=g�ra, 1=d�, 2=lewo, 3=prawo
        switch (edge)
        {
            case 0: // g�ra
                x = Random.Range(-4f, 4f);
                y = 2.5f;
                break;
            case 1: // d�
                x = Random.Range(-4f, 4f);
                y = -2.5f;
                break;
            case 2: // lewo
                x = -4f;
                y = Random.Range(-2.5f, 2.5f);
                break;
            default: // prawo
                x = 4f;
                y = Random.Range(-2.5f, 2.5f);
                break;
        }
        return new Vector2(x, y);
    }
}

