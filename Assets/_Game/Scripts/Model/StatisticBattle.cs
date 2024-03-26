using System;

[Serializable]
public struct StatisticUpgradeBattle
{
    public AllyType Type;

    public int QuantityUnit;
    public int PriceBuyUnit;

    public float Powers;
    public int PriceUpgradePowers;

    public float HP;
    public int PriceUpgradeHP;

    public float ATKSpeed;
    public int PriceUpgradeATKSpeed;
}