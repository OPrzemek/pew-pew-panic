using Managers;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TMP_Text PointsText;
    public TMP_Text LevelText;
    public TMP_Text ExpText;

    private GameManager gameManager;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }
    public void Initialize()
    {
        gameManager = GameManager.Instance;
        UpdateUI();
    }

    public void CustomUpdate()
    {

    }

    public void UpdateUI()
    {
        UpdatePoints();
        UpdateLevel();
        UpdateExp();
    }
    public void UpdatePoints() => PointsText.text = gameManager.Points.ToString();
    public void UpdateLevel() => LevelText.text = $"Lvl {gameManager.Level}";
    public void UpdateExp() => ExpText.text = $"{gameManager.Exp} / {gameManager.ExpNeeded}";
}
