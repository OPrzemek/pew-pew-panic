using Enums;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public GameState GameState;
        public GameObject MenuPanel;
        public GameObject GamePanel;
        public GameObject GameBox;
        public int Points;
        public int Level;
        public int Exp;
        public int ExpNeeded;

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
            GameState = GameState.Menu;
            MenuPanel.SetActive(true);
        }

        void Update()
        {
            if (GameState == GameState.Playing)
            {
                CustomUpdate();
                EnvironmentManager.Instance.CustomUpdate();
                UIManager.Instance.CustomUpdate();
                InputManager.Instance.CustomUpdate();
            }
        }

        private void Initialize()
        {
            EnvironmentManager.Instance.Initialize();
            UIManager.Instance.Initialize();
            InputManager.Instance.Initialize();
        }
        public void StartGame()
        {
            GameState = GameState.Playing;
            GamePanel.SetActive(true);
            Points = 0;
            Level = 1;
            Exp = 0;
            ExpNeeded = 10;
            GameBox = GameObject.FindGameObjectWithTag("GameBox");

            Initialize();

            MenuPanel.SetActive(false);
        }

        public void AddPoints(int points)
        {
            Points += points;
            Exp += points;
            CheckExp();
            UIManager.Instance.UpdateUI();
        }

        public void CheckExp()
        {
            if (Exp >= ExpNeeded)
            {
                Level++;
                ExpNeeded = (int)(ExpNeeded * 1.2f);
                Exp = 0;
                //TODO: UPGRADE TIME!!!!
            }
        }

        private void CustomUpdate()
        {
            //TODO: Remove, just for the tests
            //GameBox.transform.Rotate(0f, 0.3f, 0f, Space.Self);
        }
    }
}
