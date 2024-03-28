using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static SOPrefab;
using static SOStageData;

public class GameplayCanvasController : MonoBehaviour
{
    [Header("----- Thông tin cấp độ -----")]
    [SerializeField] TMP_Text battleMoneyTxt;
    [SerializeField] TMP_Text levelTxt;

    [Header("----- Hiển thị danh sách nâng cấp -----")]
    [SerializeField] Transform containerUpgradeList;
    [SerializeField] InfoArmyUI infoUpgradePrefab;

    List<InfoArmyUI> infoUpgradeList;
    SOPrefab SOPrefab;
    SOStageData SOStageData;

    public static GameplayCanvasController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        infoUpgradeList = new();

        SOStageData = SOContainer.Instance.GetSO<SOStageData>();
        SOPrefab = SOContainer.Instance.GetSO<SOPrefab>();

        InitDefaultStageData();
        InitUIUpgradeList();

        SetBattleMoneyText();
        SetLevelText();

        InitObserver();
    }

    void SetBattleMoneyText()
    {
        int currentBattleMoney = SessionPref.GetCurrentBattleMoney();
        battleMoneyTxt.text = "$" + currentBattleMoney;
    }

    public void SetLevelText()
    {
        int currentLevel = SessionPref.GetCurrentLevel() + 1;
        levelTxt.text = "Level " + currentLevel;
    }

    void InitDefaultStageData()
    {
        StageData[] stageDatas = SOStageData.StageDatas;

        int currentStage = SessionPref.GetCurrentStage();
        StageData currentStageData = stageDatas[currentStage];
        foreach (var type in currentStageData.DefaultAllyType)
        {
            AllyPrefabContainer allyPrefabContainer = SOPrefab.allyPrefabContainers.Where(d => d.Type == type).FirstOrDefault();

            StatisticAlly statisticAlly = new StatisticAlly
            {
                ATKSpeed = GetValueStat(StatisticType.ATK_Speed, allyPrefabContainer.StatisticDefault, SOPrefab.StatisticDefaults),
                HP = GetValueStat(StatisticType.HP, allyPrefabContainer.StatisticDefault, SOPrefab.StatisticDefaults),
                Powers = GetValueStat(StatisticType.Damage, allyPrefabContainer.StatisticDefault, SOPrefab.StatisticDefaults),
            };

            //công thức
            StatisticNextUpgrade statisticFirstUpgrade = new()
            {
                QuantityUnit = 1,
                ATKSpeed = statisticAlly.ATKSpeed + statisticAlly.ATKSpeed / 100f * 50f,
                HP = statisticAlly.HP + statisticAlly.HP / 100f * 50f,
                Powers = statisticAlly.Powers + statisticAlly.Powers / 100f * 50f,

                PriceBuyUnit = allyPrefabContainer.PriceFirstUpgrade.PriceBuyUnit,
                PriceUpgradeATKSpeed = allyPrefabContainer.PriceFirstUpgrade.PriceUpgradeATKSpeed,
                PriceUpgradeHP = allyPrefabContainer.PriceFirstUpgrade.PriceUpgradeHealth,
                PriceUpgradePowers = allyPrefabContainer.PriceFirstUpgrade.PriceUpgradePowers,

                levelATKSpeed = 0,
                levelHP = 0,
                levelPowers = 0,
            };

            StatisticBattle statisticBattle = new()
            {
                statisticAlly = statisticAlly,
                statisticNextUpgrade = statisticFirstUpgrade
            };

            SessionPref.AddStatisticBattle(type, statisticBattle);
        }
    }

    float GetValueStat(StatisticType type, StatisticDefault[] statisticAlly, List<StatisticDefault> statisticDefaults)
    {
        StatisticDefault statistic = statisticAlly.Where(s => s.Type == type).FirstOrDefault();
        if (statistic == null)
        {
            StatisticDefault statisticDefault = statisticDefaults.Where(s => s.Type == type).FirstOrDefault();
            return statisticDefault.Value;
        }
        else return statistic.Value;
    }

    void InitUIUpgradeList()
    {
        var currentData = SessionPref.GetAllCurrentStatisticBattle();
        foreach (var d in currentData)
        {
            AllyPrefabContainer allyPrefabContainer = SOPrefab.allyPrefabContainers.Where(a => a.Type == d.Key).First();

            InfoArmyUI ui = Instantiate(infoUpgradePrefab, containerUpgradeList);
            ui.Type = d.Key;
            ui.prefab = allyPrefabContainer.Prefab;
            ui.nameArmy = allyPrefabContainer.Name;
            ui.statisticAlly = allyPrefabContainer.StatisticDefault;
            ui.statisticDefaults = SOPrefab.StatisticDefaults;

            infoUpgradeList.Add(ui);
        }
    }

    void UpdateFixedColor(Color color)
    {
        foreach (var ui in infoUpgradeList) ui.UpdateFixedColor(color);
    }

    void OnChangeBattleMoney(object data)
    {
        SetBattleMoneyText();
        foreach (var i in infoUpgradeList) i.UpdateColorPriceUI();
    }

    public void OnLose()
    {
        UpdateFixedColor(Color.red);
    }

    void InitObserver()
    {
        Observer.AddEvent(ObserverKey.ON_CHANGE_BATTLE_MONEY, OnChangeBattleMoney);
    }

    void RemoveObserser()
    {
        Observer.RemoveEvent(ObserverKey.ON_CHANGE_BATTLE_MONEY);
    }

    private void OnDestroy()
    {
        RemoveObserser();
    }
}