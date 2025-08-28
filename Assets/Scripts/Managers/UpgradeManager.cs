using Enums;
using Managers;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    public List<Upgrade> CurrentUpgrades;

    public List<UpgradeData> UpgradesData;

    public GameObject UpgradePanel;
    public GameObject UpgradePrefab;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }

    public void Initialize()
    {
        UpgradePanel.SetActive(false);
        CurrentUpgrades = new();
    }

    public void CustomUpdate()
    {

    }

    public void LevelUp()
    {
        GameManager.Instance.GameState = GameState.Paused;
        UpgradePanel.SetActive(true);
        Music.Instance.source.volume /= 4f;
        for (int i = 0; i < 3; i++)
        {
            GenerateCard(i + 49);
        }
    }

    public void GenerateCard(int keyCode)
    {
        int weaponIndex = Random.Range(0, Ship.Instance.weapons.Count);
        int upgradeIndex = Random.Range(0, UpgradesData.Count);
        Upgrade upgrade = Instantiate(UpgradePrefab, UpgradePanel.transform).GetComponent<Upgrade>();
        upgrade.inputKeyCode = (KeyCode)keyCode;
        upgrade.InfoText.text = $"[{upgrade.inputKeyCode}]\r\n For Weapon {weaponIndex + 1} :\r\n {UpgradesData[upgradeIndex].Info}";
        upgrade.TakeButton.onClick.AddListener(delegate { UpgradeWeapon(Ship.Instance.weapons[weaponIndex], UpgradesData[upgradeIndex].UpgradeType); });
        CurrentUpgrades.Add(upgrade);

    }

    public void UpgradeWeapon(Weapon weapon, UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.CooldownReduction:
                weapon.cooldown = weapon.cooldown * 0.8f;
                break;
            case UpgradeType.Speed:
                weapon.speed = (int)Mathf.Ceil(weapon.speed* 1.2f);
                break;
            case UpgradeType.Damage:
                weapon.damage = (int)Mathf.Ceil(weapon.damage * 1.2f);
                break;
            case UpgradeType.Spread:
                weapon.spreadAngle = weapon.spreadAngle * 1.2f;
                break;
            case UpgradeType.ProjectileCount:
                weapon.projectilesPerShot = (int)Mathf.Ceil(weapon.projectilesPerShot * 1.2f);
                break;
            case UpgradeType.NewWeapon:
                //TODO
                break;
        }
        foreach (Transform child in UpgradePanel.transform)
        {
            Destroy(child.gameObject);
        }
        CurrentUpgrades.Clear();
        GameManager.Instance.GameState = GameState.Playing;
        Music.Instance.source.volume *= 4f;
        UpgradePanel.SetActive(false);
    }
}
