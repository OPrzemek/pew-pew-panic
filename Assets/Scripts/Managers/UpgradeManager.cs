using Enums;
using Managers;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    public List<UpgradeData> Upgrades;

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
            GenerateCard();
        }
    }

    public void GenerateCard()
    {
        int weaponIndex = Random.Range(0, Ship.Instance.weapons.Count);
        int upgradeIndex = Random.Range(0, Upgrades.Count);
        Upgrade upgrade = Instantiate(UpgradePrefab, UpgradePanel.transform).GetComponent<Upgrade>();
        upgrade.InfoText.text = $"For Weapon {weaponIndex + 1} :\r\n {Upgrades[upgradeIndex].Info}";
        upgrade.TakeButton.onClick.AddListener(delegate { UpgradeWeapon(Ship.Instance.weapons[weaponIndex], Upgrades[upgradeIndex].UpgradeType); });
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
        GameManager.Instance.GameState = GameState.Playing;
        Music.Instance.source.volume *= 4f;
        UpgradePanel.SetActive(false);
    }
}
