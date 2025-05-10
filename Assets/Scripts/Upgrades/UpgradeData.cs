using Enums;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public string Info;

    public UpgradeType UpgradeType;
}