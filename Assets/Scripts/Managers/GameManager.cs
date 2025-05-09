using Enums;
using UnityEngine;

namespace Managers
{
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

        void Start()
        {
            Initialize();
            EnvironmentManager.Instance.Initialize();
        }

        void Update()
        {
            CustomUpdate();
            EnvironmentManager.Instance.CustomUpdate();
        }
        private void Initialize()
        {
            //TODO: Start with menu state
            gameState = GameState.Playing;
            GameBox = GameObject.FindGameObjectWithTag("GameBox");
        }

        private void CustomUpdate()
        {
            //TODO: Remove, just for the tests
            //GameBox.transform.Rotate(0f, 0.3f, 0f, Space.Self);
        }
    }
}
