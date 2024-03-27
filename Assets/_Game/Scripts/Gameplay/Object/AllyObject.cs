using UnityEngine;

public class AllyObject : CharacterObject
{
    [SerializeField] AllyType type;

    protected override void Death()
    {
        base.Death();
        LevelManager.Instance.OnAllyDeath(this);
    }
}