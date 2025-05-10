using UnityEngine;

namespace Enums
{
    public enum GameState
    {
        Menu,
        Playing,
        Paused,
        Won,
        Lost
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
        Speed
    }
}
