using Enums;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public GameState GameState;
        public GameObject MenuPanel;
        public GameObject GamePanel;
        public GameObject EndGamePanel;
        public TMP_Text EndGameText;

        public GameObject GameBox;
        public GameObject EnemyHolder;
        public GameObject ProjectileHolder;
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
            GamePanel.SetActive(false);
            EndGamePanel.SetActive(false);
        }

        void Update()
        {
            if (GameState == GameState.Playing)
            {
                CustomUpdate();
                EnvironmentManager.Instance.CustomUpdate();
                Ship.Instance.CustomUpdate();
                UpgradeManager.Instance.CustomUpdate();
                EnemyManager.Instance.CustomUpdate();
                UIManager.Instance.CustomUpdate();
                InputManager.Instance.CustomUpdate();
            }
        }

        private void Initialize()
        {
            EnvironmentManager.Instance.Initialize();
            Ship.Instance.Initialize();
            UpgradeManager.Instance.Initialize();
            EnemyManager.Instance.Initialize();
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
            EnemyHolder = GameObject.FindGameObjectWithTag("EnemyHolder");
            ProjectileHolder = GameObject.FindGameObjectWithTag("ProjectileHolder");
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
                ExpNeeded = (int)(ExpNeeded * 1.4f);
                Exp = 0;
                //UPGRADE TIME!!!!
                UpgradeManager.Instance.LevelUp();
                EnemyManager.Instance.enemyHealth++;
                EnemyManager.Instance.spawnInterval *= 0.98f;
                if(EnemyManager.Instance.spawnInterval < 1f)
                    EnemyManager.Instance.spawnInterval = 1f;
            }
        }

        private void CustomUpdate()
        {
            //TODO: Remove, just for the tests
            //GameBox.transform.Rotate(0f, 0.3f, 0f, Space.Self);
        }
    }
}
