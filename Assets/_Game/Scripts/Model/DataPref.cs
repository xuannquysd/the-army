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
    Dictionary<AllyType, StatisticBattle> staticBattles;

    public List<AllyObject> AllyObjects { get => allyObjects; set => allyObjects = value; }
    public int CurrentMoney { get => currentMoney; set => currentMoney = value; }
    public int CurrentStage { get => currentStage; set => currentStage = value; }
    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }
    public Dictionary<AllyType, StatisticBattle> StaticBattles { get => staticBattles; set => staticBattles = value; }

    public InGameData()
    {
        CurrentStage = 0;
        CurrentLevel = 0;

        CurrentMoney = 0;
        AllyObjects = new();
        StaticBattles = new()
        {
            /*{
                AllyType.WARRIOR,
                new StatisticBattle(GameplayConstance.DEFAULT_UPGRADE_BATTLE, GameplayConstance.DEFAULT_STATISTIC_ALLY)
            },
            {
                AllyType.BIG_WARRIOR,
                new StatisticBattle(GameplayConstance.DEFAULT_UPGRADE_BATTLE, GameplayConstance.DEFAULT_STATISTIC_ALLY)
            },*/
        };
    }
}