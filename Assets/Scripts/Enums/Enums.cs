using UnityEngine;

namespace Enums
{
    public enum GameState
    {
        Menu,
        Playing,
        Paused,
        End,
    }
    public enum EnemyColor
    {
        Red,
        Orange
    }
    public enum PatternType
    {
        Rectangle,
        Circle,
        Bounce
    }

    public enum UpgradeType
    {
        CooldownReduction,
        Damage,
        Speed,
        ProjectileCount,
        Spread,
        NewWeapon
    }
}
