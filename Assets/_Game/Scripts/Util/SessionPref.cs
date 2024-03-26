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
        GetInGameData().AllyObjects.Clear();
        SetInGameData(new());
    }
    
    public static int GetCurrentInGameMoney()
    {
        return _gameData.CurrentMoney;
    }
}