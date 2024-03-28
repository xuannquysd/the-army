using UnityEngine;

public class AllyObject : CharacterObject
{
    [SerializeField] AllyType type;

    public AllyType Type { get => type; set => type = value; }

    private void Start()
    {
        base.Start();
        SetStatisticBuff();
    }

    public void SetStatisticBuff()
    {
        StatisticAlly statisticAlly = SessionPref.GetStatisticAlly(Type);
        damage = statisticAlly.Powers;
        speedAttack = statisticAlly.ATKSpeed;

        float diffHP = statisticAlly.HP - RawHPReact;
        RawHPReact = statisticAlly.HP;
        CurrentHP += diffHP;
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