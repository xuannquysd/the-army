using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu(fileName = "SOPrefab", menuName = "Game Datas/SOPrefab")]
public class SOPrefab : ScriptableObject
{
#if UNITY_EDITOR
    public string[] StatisticType;
#endif

    public List<StatisticDefault> StatisticDefaults;
    public AllyPrefabContainer[] allyPrefabContainers;

    [Serializable]
    public struct AllyPrefabContainer
    {
        public string Name;
        public AllyType Type;
        public Sprite Icon;
        public AllyObject Prefab;

        public StatisticDefault[] StatisticDefault;
        public PriceFirstUpgrade PriceFirstUpgrade;
    }

    [Serializable]
    public struct PriceFirstUpgrade
    {
        public int PriceBuyUnit;
        public int PriceUpgradePowers;
        public int PriceUpgradeHealth;
        public int PriceUpgradeATKSpeed;
    }

    [Serializable]
    public class StatisticDefault
    {
        public StatisticType Type;
        public float Value;
    }

#if UNITY_EDITOR
    [ContextMenu("Update Statistic Type")]
    public void WriteEnum()
    {
        string allType = "";
        foreach(var s in StatisticType) allType += s.Replace(" ", "_") + ", ";

        string text = "public enum StatisticType { " + allType + " }";

        string path = "Assets/_Game/Scripts/Constance/StatisticTypeEnum.cs";
        var lines = File.ReadAllLines(path);
        lines[0] = text;
        File.WriteAllLines(path, lines);

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
    }

    [ContextMenu("Update Statistic Default")]
    public void UpdateStatDefault()
    {
        StatisticDefaults ??= new();
        foreach (var name in Enum.GetValues(typeof(StatisticType)))
        {
            StatisticType type = (StatisticType)name;
            if (StatisticDefaults.Where(s => s.Type == type).FirstOrDefault() == null)
            {
                StatisticDefaults.Add(new StatisticDefault
                {
                    Type = type,
                    Value = 0
                });
            }
        }
    }
#endif
}