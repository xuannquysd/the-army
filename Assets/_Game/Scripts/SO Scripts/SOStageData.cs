using MyBox;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SOStageData", menuName = "Game Datas/SOStageData")]
public class SOStageData : ScriptableObject
{
    public StageData[] StageDatas;
    public GameObject[] levelRandomList;

    [Serializable]
    public struct StageData
    {
        public AllyType[] DefaultAllyType;
        public LevelData[] levelDatas;
    }

    [Serializable]
    public struct LevelData
    {
        public bool isRandom;

        [ConditionalField(nameof(isRandom), true)]
        public GameObject Prefab;
    }
}