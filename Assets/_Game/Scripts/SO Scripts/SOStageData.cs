using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SOStageData", menuName = "Game Datas/SOStageData")]
public class SOStageData : ScriptableObject
{
    public StageData[] StageDatas;

    [Serializable]
    public struct StageData
    {
        public AllyType[] DefaultAllyType;
    }
}