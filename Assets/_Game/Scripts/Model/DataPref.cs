using System;
using System.Collections.Generic;

[Serializable]
public class CurrentData
{

}

[Serializable]
public class InGameData
{
    int currentStage, currentLevel;

    int currentMoney;
    List<AllyObject> allyObjects;
    List<StatisticUpgradeBattle> staticBattles;

    public List<AllyObject> AllyObjects { get => allyObjects; set => allyObjects = value; }
    public List<StatisticUpgradeBattle> StaticBattles { get => staticBattles; set => staticBattles = value; }
    public int CurrentMoney { get => currentMoney; set => currentMoney = value; }
    public int CurrentStage { get => currentStage; set => currentStage = value; }
    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }

    public InGameData()
    {
        CurrentStage = 0;
        CurrentLevel = 0;

        CurrentMoney = 0;
        AllyObjects = new();
        StaticBattles = new()
        {
            GameplayConstance.DEFAULT_UPGRADE_BATTLE
        };
    }
}