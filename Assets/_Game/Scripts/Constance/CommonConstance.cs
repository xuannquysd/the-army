using UnityEngine;

public class ObserverKey
{
    public static string ON_CHANGE_STATISTIC_BATTLE = "On change statistic battle";
    public static string ON_CHANGE_BATTLE_MONEY = "On change battle money";
}

public class GameplayConstance
{
    public static StatisticNextUpgrade DEFAULT_UPGRADE_BATTLE = new()
    {
        QuantityUnit = 1,
        PriceBuyUnit = 3,

        Powers = 1,
        PriceUpgradePowers = 3,

        HP = 1,
        PriceUpgradeHP = 3,

        ATKSpeed = 1,
        PriceUpgradeATKSpeed = 3
    };
    public static StatisticAlly DEFAULT_STATISTIC_ALLY = new()
    {
        HP = 1,
        Powers = 1,
        ATKSpeed = 1
    };

    public static Vector3 SPAWN_POSITION = new(0f, 2f, 0f);
}