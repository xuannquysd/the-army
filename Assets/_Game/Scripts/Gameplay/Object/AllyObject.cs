public class AllyObject : CharacterObject
{
    protected override void Death()
    {
        base.Death();
        LevelManager.Instance.OnAllyDeath(this);
    }
}