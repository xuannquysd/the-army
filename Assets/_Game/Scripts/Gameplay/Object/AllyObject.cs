using UnityEngine;

public class AllyObject : CharacterObject
{
    [SerializeField] AllyType type;

    private void Start()
    {
        base.Start();
        SetStatisticBuff();
    }

    public void SetStatisticBuff()
    {
        StatisticAlly statisticAlly = SessionPref.GetStatisticAlly(type);

    }

    public override void OnKillTarget()
    {
        SessionPref.AddBattleMoney(Mathf.RoundToInt(currentTarget.RawHP));
        base.OnKillTarget();
    }

    protected override void Death()
    {
        base.Death();
        LevelManager.Instance.OnAllyDeath(this);
    }
}