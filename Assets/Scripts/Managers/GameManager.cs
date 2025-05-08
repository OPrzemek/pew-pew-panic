using Enums;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState gameState;
    public GameObject GameBox;

    private void Awake()
    {
        //Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        //TODO: Start with menu state
        gameState = GameState.Playing;
        GameBox = GameObject.FindGameObjectWithTag("GameBox");
    }

    // Update is called once per frame
    void Update()
    {
        CustomUpdate();
    }

    private void CustomUpdate()
    {
        //TODO: Remove, just for the tests
        //GameBox.transform.Rotate(0f, 0.3f, 0f, Space.Self);
    }
}
