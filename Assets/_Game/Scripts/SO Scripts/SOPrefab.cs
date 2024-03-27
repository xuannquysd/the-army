using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SOPrefab", menuName = "Game Datas/SOPrefab")]
public class SOPrefab : ScriptableObject
{
    public AllyPrefabContainer[] allyPrefabContainers;

    [Serializable]
    public struct AllyPrefabContainer
    {
        public AllyType Type;
        public string Name;
        public Sprite Icon;
        public AllyObject Prefab;

        public StatisticNextUpgrade StatisticFirstUpgrade;
    }
}