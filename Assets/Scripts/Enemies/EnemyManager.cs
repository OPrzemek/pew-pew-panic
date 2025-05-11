using Enums;
using Managers;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //Ustawienia spawnowania
    public GameObject[] patternPrefabs;      // Prefaby wzorc�w (Rectangle, Circle, Bounce)
    public Transform playerTransform;        // Gracz � cel dla pattern�w
    public float spawnInterval = 5f;         // Co ile sekund spawnowa� pattern

    private float timer = 0f;


    void Update()
    {
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

        // Ustaw losow� pozycj� pojawienia si� (np. nad ekranem)
        Vector2 spawnPos = GetRandomSpawnPosition();

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
                x = Random.Range(-4.5f, 4.5f);
                y = 4.8f;
                break;
            case 1: // d�
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

