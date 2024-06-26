﻿using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static SOPrefab;

public class InfoArmyUI : MonoBehaviour
{
    [SerializeField] TMP_Text title;
    [SerializeField] UpgradeButton buyUnitBtn, upgradePowersBtn, upgradeHealthBtn, upgradeAtkSpeedBtn;

    [HideInInspector] public AllyType Type;
    [HideInInspector] public AllyObject prefab;
    [HideInInspector] public string nameArmy;
    [HideInInspector] public StatisticDefault[] statisticAlly;
    [HideInInspector] public List<StatisticDefault> statisticDefaults;

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
        SetStasticAlly(army);
        army.SetStatisticBuff();

        LevelManager.Instance.OnChangeQuantityAlly(army);

        statisticNextUpgrade.QuantityUnit++;
        //Sẽ sửa theo công thức
        int nextPrice = Mathf.RoundToInt(currentPrice + currentPrice / 100f * 50f);
        statisticNextUpgrade.PriceBuyUnit = nextPrice;

        SessionPref.SetStatisticNextUpgrade(Type, statisticNextUpgrade);

        UpdateUIBuyUnitBtn(statisticNextUpgrade);
        SessionPref.AddBattleMoney(-currentPrice);
    }

    void SetStasticAlly(AllyObject allyObject)
    {
        allyObject.RawDamage = GetValueStat(StatisticType.Damage);
        allyObject.RawHP = GetValueStat(StatisticType.HP);
        allyObject.RawRadiusAttack = GetValueStat(StatisticType.Radius_Attack);
        allyObject.RawSpeedAttack = GetValueStat(StatisticType.ATK_Speed);
        allyObject.RawSpeedMove = GetValueStat(StatisticType.Speed_Move);
        allyObject.RawSpeedRotate = GetValueStat(StatisticType.Speed_Rotate);
        allyObject.RawDefend = GetValueStat(StatisticType.Defend);
    }

    float GetValueStat(StatisticType type)
    {
        StatisticDefault statistic = statisticAlly.Where(s => s.Type == type).FirstOrDefault();
        if (statistic == null)
        {
            StatisticDefault statisticDefault = statisticDefaults.Where(s => s.Type == type).FirstOrDefault();
            return statisticDefault.Value;
        }
        else return statistic.Value;
    }

    void OnClickUpgradePowers()
    {
        StatisticNextUpgrade statisticNextUpgrade = SessionPref.GetStatisticNextUpgrade(Type);
        int currentMoney = SessionPref.GetCurrentBattleMoney();
        int currentPrice = statisticNextUpgrade.PriceUpgradePowers;

        if (currentMoney < currentPrice) return;
        StatisticAlly statisticAlly = SessionPref.GetStatisticAlly(Type);

        float currentPowers = statisticNextUpgrade.Powers;
        statisticAlly.Powers = currentPowers;

        int levelPower = statisticNextUpgrade.levelPowers;
        float nextPower = currentPowers + currentPowers / 100f * 50f;
        int nextPrice = currentPrice + Mathf.RoundToInt(currentPrice / 100f * 50f);
        levelPower++;

        statisticNextUpgrade.PriceUpgradePowers = nextPrice;
        statisticNextUpgrade.Powers = nextPower;
        statisticNextUpgrade.levelPowers = levelPower;

        SessionPref.SetStatisticAlly(Type, statisticAlly);
        SessionPref.SetStatisticNextUpgrade(Type, statisticNextUpgrade);

        LevelManager.Instance.UpgradeStaticsArmy(Type);
        UpdateUIUpgradePoweresBtn(statisticNextUpgrade);
        SessionPref.AddBattleMoney(-currentPrice);
    }

    void OnClickUpgradeHealth()
    {
        StatisticNextUpgrade statisticNextUpgrade = SessionPref.GetStatisticNextUpgrade(Type);
        int currentMoney = SessionPref.GetCurrentBattleMoney();
        int currentPrice = statisticNextUpgrade.PriceUpgradeHP;

        if (currentMoney < currentPrice) return;
        StatisticAlly statisticAlly = SessionPref.GetStatisticAlly(Type);

        float currentHealth = statisticNextUpgrade.HP;
        statisticAlly.HP = currentHealth;

        int levelHealth = statisticNextUpgrade.levelHP;
        float nextHealth = currentHealth + currentHealth / 100f * 50f;
        int nextPrice = currentPrice + Mathf.RoundToInt(currentPrice / 100f * 50f);
        levelHealth++;

        statisticNextUpgrade.PriceUpgradeHP = nextPrice;
        statisticNextUpgrade.HP = nextHealth;
        statisticNextUpgrade.levelHP = levelHealth;

        SessionPref.SetStatisticAlly(Type, statisticAlly);
        SessionPref.SetStatisticNextUpgrade(Type, statisticNextUpgrade);

        LevelManager.Instance.UpgradeStaticsArmy(Type);
        UpdateUIUpgradeHealthBtn(statisticNextUpgrade);
        SessionPref.AddBattleMoney(-currentPrice);
    }

    void OnClickUpgradeAtkSpeed()
    {
        StatisticNextUpgrade statisticNextUpgrade = SessionPref.GetStatisticNextUpgrade(Type);
        int currentMoney = SessionPref.GetCurrentBattleMoney();
        int currentPrice = statisticNextUpgrade.PriceUpgradeATKSpeed;

        if (currentMoney < currentPrice) return;
        StatisticAlly statisticAlly = SessionPref.GetStatisticAlly(Type);

        float currentATKSpeed = statisticNextUpgrade.ATKSpeed;
        statisticAlly.ATKSpeed = currentATKSpeed;

        int levelAtkSpeed = statisticNextUpgrade.levelATKSpeed;
        float nextATKSpeed = currentATKSpeed + currentATKSpeed / 100f * 50f;
        int nextPrice = currentPrice + Mathf.RoundToInt(currentPrice / 100f * 50f);
        levelAtkSpeed++;

        statisticNextUpgrade.PriceUpgradeATKSpeed = nextPrice;
        statisticNextUpgrade.ATKSpeed = nextATKSpeed;
        statisticNextUpgrade.levelATKSpeed = levelAtkSpeed;

        SessionPref.SetStatisticAlly(Type, statisticAlly);
        SessionPref.SetStatisticNextUpgrade(Type, statisticNextUpgrade);

        LevelManager.Instance.UpgradeStaticsArmy(Type);
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