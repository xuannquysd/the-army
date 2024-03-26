public class EnemyObject : CharacterObject
{
    protected override void Death()
    {
        base.Death();
        LevelManager.Instance.OnEnemyDeath(this);
    }
}