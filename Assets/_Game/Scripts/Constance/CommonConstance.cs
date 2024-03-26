public class ObserverKey
{
    public static string ON_CHANGE_STATISTIC_BATTLE = "On change statistic battle";
}

public class GameplayConstance
{
    public static StatisticUpgradeBattle DEFAULT_UPGRADE_BATTLE = new()
    {
        Type = AllyType.WARRIOR,

        QuantityUnit = 1,
        PriceBuyUnit = 3,

        Powers = 1,
        PriceUpgradePowers = 3,

        HP = 1,
        PriceUpgradeHP = 3,

        ATKSpeed = 1,
        PriceUpgradeATKSpeed = 3
    };
}