using System;

[Serializable]
public struct StatisticNextUpgrade
{
    public int QuantityUnit;
    public int PriceBuyUnit;

    public float Powers;
    public int PriceUpgradePowers;

    public float HP;
    public int PriceUpgradeHP;

    public float ATKSpeed;
    public int PriceUpgradeATKSpeed;
}

[Serializable]
public struct StatisticAlly
{
    public float Powers;
    public float HP;
    public float ATKSpeed;
}

[Serializable]
public struct StatisticBattle
{
    public StatisticNextUpgrade statisticNextUpgrade;
    public StatisticAlly statisticAlly;

    public StatisticBattle(StatisticNextUpgrade statisticNextUpgrade, StatisticAlly statisticAlly)
    {
        this.statisticNextUpgrade = statisticNextUpgrade;
        this.statisticAlly = statisticAlly;
    }
}