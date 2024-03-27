using MyBox;
using System;
using System.Collections.Generic;

public static class SessionPref
{
    static CurrentData _currentData;
    static InGameData _gameData;

    public static InGameData GetInGameData()
    {
        _gameData ??= new();
        return _gameData;
    }

    public static List<AllyObject> GetSaveAlly()
    {
        return GetInGameData().AllyObjects;
    }

    public static void SetInGameData(InGameData inGameData)
    {
        _gameData = inGameData;
    }

    public static void SaveAlly(List<AllyObject> allyObjects)
    {
        GetInGameData().AllyObjects = allyObjects;
    }

    public static void ClearSaveData()
    {
        SetInGameData(new());
    }

    public static int GetCurrentBattleMoney()
    {
        return GetInGameData().CurrentMoney;
    }

    public static void AddBattleMoney(int value)
    {
        GetInGameData().CurrentMoney += value;
        Observer.Notify(ObserverKey.ON_CHANGE_BATTLE_MONEY);
    }

    public static void SetBattleMoney(int value)
    {
        GetInGameData().CurrentMoney = value;
        Observer.Notify(ObserverKey.ON_CHANGE_BATTLE_MONEY);
    }

    public static StatisticBattle GetCurrentStatisticBattle(AllyType type)
    {
        var dictionary = GetInGameData().StaticBattles;
        return dictionary.GetOrAdd(type, new StatisticBattle());
    }

    public static Dictionary<AllyType, StatisticBattle> GetAllCurrentStatisticBattle()
    {
        return GetInGameData().StaticBattles;
    }

    public static StatisticNextUpgrade GetStatisticNextUpgrade(AllyType type)
    {
        return GetInGameData().StaticBattles[type].statisticNextUpgrade;
    }

    public static void SetStatisticNextUpgrade(AllyType type, StatisticNextUpgrade statisticNextUpgrade)
    {
        StatisticBattle statisticBattle = GetInGameData().StaticBattles[type];
        statisticBattle.statisticNextUpgrade = statisticNextUpgrade;

        GetInGameData().StaticBattles[type] = statisticBattle;
    }

    public static StatisticAlly GetStatisticAlly(AllyType type)
    {
        return GetInGameData().StaticBattles[type].statisticAlly;
    }

    public static void SetStatisticAlly(AllyType type, StatisticAlly statisticAlly)
    {
        StatisticBattle statisticBattle = GetInGameData().StaticBattles[type];
        statisticBattle.statisticAlly = statisticAlly;

        GetInGameData().StaticBattles[type] = statisticBattle;
    }

    public static void AddStatisticBattle(AllyType type, StatisticBattle statisticBattle)
    {
        GetInGameData().StaticBattles.Add(type, statisticBattle);
    }

    public static int GetCurrentStage()
    {
        return GetInGameData().CurrentStage;
    }

    public static void SetCurrentStage(int stage)
    {
        GetInGameData().CurrentStage = stage;
    }

    public static int GetCurrentLevel()
    {
        return GetInGameData().CurrentLevel;
    }

    public static void SetCurrentLevel(int level)
    {
        GetInGameData().CurrentLevel = level;
    }

    public static void NextLevel()
    {
        GetInGameData().CurrentLevel++;
    }

}