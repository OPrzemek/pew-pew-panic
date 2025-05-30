using Enums;
using Managers;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    //Ustawienia spawnowania
    public GameObject[] patternPrefabs;      // Prefaby wzorców (Rectangle, Circle, Bounce)
    public Transform playerTransform;        // Gracz – cel dla patternów
    public float spawnInterval = 15f;         // Co ile sekund spawnowaæ pattern
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
            SpawnRandomPattern();//// Spawnuj now¹ falê
            timer = 0f;
        }
        // Klawisz testowy: naciœnij P, ¿eby natychmiast zespawnowaæ pattern
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnRandomPattern();
        }
    }

    void SpawnRandomPattern()
    {
        // Wybiera losowy wzorzec z listy prefabów
        int index = Random.Range(0, patternPrefabs.Length);
        GameObject selectedPrefab = patternPrefabs[index];

        // Ustaw pozycjê pojawienia siê (je¿eli nie jest to odbijaj¹cy siê pattern, podaj pozycjê gracza)
        Vector2 spawnPos = Ship.Instance.transform.position;
        if(index == patternPrefabs.Length - 1)
            spawnPos = GetRandomSpawnPosition();

        // Stwórz instancjê wzorca
        GameObject instance = Instantiate(selectedPrefab, spawnPos, Quaternion.identity, GameManager.Instance.EnemyHolder.transform);

        // ZnajdŸ EnemyPattern na tym obiekcie
        EnemyPattern patternScript = instance.GetComponent<EnemyPattern>();
        if (patternScript != null)
        {
            patternScript.target = playerTransform; // Przeka¿ gracza jako cel
        }

    }
    Vector2 GetRandomSpawnPosition()
    {
        float x, y;
        int edge = Random.Range(0, 4); // 0=góra, 1=dó³, 2=lewo, 3=prawo
        switch (edge)
        {
            case 0: // góra
                x = Random.Range(-4f, 4f);
                y = 2.5f;
                break;
            case 1: // dó³
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

