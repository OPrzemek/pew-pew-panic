using Enums;
using Managers;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //Ustawienia spawnowania
    public GameObject[] patternPrefabs;      // Prefaby wzorców (Rectangle, Circle, Bounce)
    public Transform playerTransform;        // Gracz – cel dla patternów
    public float spawnInterval = 5f;         // Co ile sekund spawnowaæ pattern

    private float timer = 0f;


    void Update()
    {
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

        // Ustaw losow¹ pozycjê pojawienia siê (np. nad ekranem)
        Vector2 spawnPos = GetRandomSpawnPosition();

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
                x = Random.Range(-4.5f, 4.5f);
                y = 4.8f;
                break;
            case 1: // dó³
                x = Random.Range(-4.5f, 4.5f);
                y = -4.8f;
                break;
            case 2: // lewo
                x = -5f;
                y = Random.Range(-3.5f, 3.5f);
                break;
            default: // prawo
                x = 5f;
                y = Random.Range(-3.5f, 3.5f);
                break;
        }
        return new Vector2(x, y);
    }
}

