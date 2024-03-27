using System;
using TMPro;
using UnityEngine;

public class InfoArmyUI : MonoBehaviour
{
    [SerializeField] TMP_Text title;
    [SerializeField] UpgradeButton buyUnitBtn, upgradePowersBtn, upgradeHealthBtn, upgradeAtkSpeedBtn;
    
    [HideInInspector] public AllyType Type;
    [HideInInspector] public AllyObject prefab;
    [HideInInspector] public string nameArmy;

    private void Awake()
    {
        buyUnitBtn.AddListener(() => OnClickButton(OnClickBuyUnit));
        upgradePowersBtn.AddListener(() => OnClickButton(OnClickUpgradePowers));
        upgradeHealthBtn.AddListener(() => OnClickButton(OnClickUpgradeHealth));
        upgradeAtkSpeedBtn.AddListener(() => OnClickButton(OnClickUpgradeAtkSpeed));
    }

    private void Start()
    {
        title.text = nameArmy;
        UpdateUIUpgradeBtn();
        UpdateColorPriceUI();
    }

    void OnClickButton(Action callBack)
    {
        if (GameManager.Instance.GameState != GameState.PLAYING) return;
        callBack?.Invoke();
    }

    void OnClickBuyUnit()
    {
        StatisticNextUpgrade statisticNextUpgrade = SessionPref.GetStatisticNextUpgrade(Type);
        int currentMoney = SessionPref.GetCurrentBattleMoney();
        int currentPrice = statisticNextUpgrade.PriceBuyUnit;

        if (currentMoney < currentPrice) return;

        AllyObject army = Instantiate(prefab, GameplayConstance.SPAWN_POSITION, Quaternion.identity);
        LevelManager.Instance.OnChangeQuantityAlly(army);

        statisticNextUpgrade.QuantityUnit++;
        //Sẽ sửa theo công thức
        int nextPrice = Mathf.RoundToInt(currentPrice + currentPrice / 100f * 50f);
        statisticNextUpgrade.PriceBuyUnit = nextPrice;

        SessionPref.SetStatisticNextUpgrade(Type, statisticNextUpgrade);

        UpdateUIBuyUnitBtn(statisticNextUpgrade);
        SessionPref.AddBattleMoney(-currentPrice);
    }

    void OnClickUpgradePowers()
    {
        StatisticNextUpgrade statisticNextUpgrade = SessionPref.GetStatisticNextUpgrade(Type);
        int currentMoney = SessionPref.GetCurrentBattleMoney();
        int currentPrice = statisticNextUpgrade.PriceUpgradePowers;

        if (currentMoney < currentPrice) return;

        UpdateUIUpgradePoweresBtn(statisticNextUpgrade);
        SessionPref.AddBattleMoney(-currentPrice);
    }

    void OnClickUpgradeHealth()
    {
        StatisticNextUpgrade statisticNextUpgrade = SessionPref.GetStatisticNextUpgrade(Type);
        int currentMoney = SessionPref.GetCurrentBattleMoney();
        int currentPrice = statisticNextUpgrade.PriceUpgradeHP;

        if (currentMoney < currentPrice) return;

        UpdateUIUpgradeHealthBtn(statisticNextUpgrade);
        SessionPref.AddBattleMoney(-currentPrice);
    }

    void OnClickUpgradeAtkSpeed()
    {
        StatisticNextUpgrade statisticNextUpgrade = SessionPref.GetStatisticNextUpgrade(Type);
        int currentMoney = SessionPref.GetCurrentBattleMoney();
        int currentPrice = statisticNextUpgrade.PriceUpgradeATKSpeed;

        if (currentMoney < currentPrice) return;

        UpdateUIUpgradeATKSpeedBtn(statisticNextUpgrade);
        SessionPref.AddBattleMoney(-currentPrice);
    }

    void UpdateUIUpgradeBtn()
    {
        StatisticNextUpgrade statisticNextUpgrade = SessionPref.GetStatisticNextUpgrade(Type);

        UpdateUIBuyUnitBtn(statisticNextUpgrade);
        UpdateUIUpgradePoweresBtn(statisticNextUpgrade);
        UpdateUIUpgradeHealthBtn(statisticNextUpgrade);
        UpdateUIUpgradeATKSpeedBtn(statisticNextUpgrade);
    }

    void UpdateUIBuyUnitBtn(StatisticNextUpgrade statisticNextUpgrade)
    {
        int quantity = statisticNextUpgrade.QuantityUnit;
        int price = statisticNextUpgrade.PriceBuyUnit;

        buyUnitBtn.UpdateUI(quantity, price);
    }

    void UpdateUIUpgradePoweresBtn(StatisticNextUpgrade statisticNextUpgrade)
    {
        float statistic = statisticNextUpgrade.Powers;
        int price = statisticNextUpgrade.PriceUpgradePowers;

        upgradePowersBtn.UpdateUI(statistic, price);
    }

    void UpdateUIUpgradeHealthBtn(StatisticNextUpgrade statisticNextUpgrade)
    {
        float statistic = statisticNextUpgrade.HP;
        int price = statisticNextUpgrade.PriceUpgradeHP;

        upgradeHealthBtn.UpdateUI(statistic, price);
    }

    void UpdateUIUpgradeATKSpeedBtn(StatisticNextUpgrade statisticNextUpgrade)
    {
        float statistic = statisticNextUpgrade.ATKSpeed;
        int price = statisticNextUpgrade.PriceUpgradeATKSpeed;

        upgradeAtkSpeedBtn.UpdateUI(statistic, price);
    }

    public void UpdateColorPriceUI()
    {
        buyUnitBtn.UpdateColor();
        upgradePowersBtn.UpdateColor();
        upgradeHealthBtn.UpdateColor();
        upgradeAtkSpeedBtn.UpdateColor();
    }

    public void UpdateFixedColor(Color color)
    {
        buyUnitBtn.UpdateFixedColor(color);
        upgradePowersBtn.UpdateFixedColor(color);
        upgradeHealthBtn.UpdateFixedColor(color);
        upgradeAtkSpeedBtn.UpdateFixedColor(color);
    }
}