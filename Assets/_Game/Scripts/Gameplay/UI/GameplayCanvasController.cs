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

    SOPrefab SOPrefab;
    SOStageData SOStageData;

    private void Start()
    {
        SOStageData = SOContainer.Instance.GetSO<SOStageData>();
        SOPrefab = SOContainer.Instance.GetSO<SOPrefab>();

        InitDefaultStageData();
        InitUIUpgradeList();
    }

    void InitDefaultStageData()
    {
        StageData[] stageDatas = SOStageData.StageDatas;

        int currentStage = SessionPref.GetCurrentStage();
        StageData currentStageData = stageDatas[currentStage];
        foreach(var type in currentStageData.DefaultAllyType)
        {
            StatisticNextUpgrade statisticFirstUpgrade = SOPrefab.allyPrefabContainers.Where(d => d.Type == type).FirstOrDefault().StatisticFirstUpgrade;
            StatisticBattle statisticBattle = new()
            {
                statisticAlly = GameplayConstance.DEFAULT_STATISTIC_ALLY,
                statisticNextUpgrade = statisticFirstUpgrade
            };

            SessionPref.AddStatisticBattle(type, statisticBattle);
        }
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
        }
    }
}