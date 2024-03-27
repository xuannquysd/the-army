using UnityEngine;

public class EnemyObject : CharacterObject
{
    protected override void Death()
    {
        GameObject deathEffectPrefab = EffectManager.Instance.GetEffect<EffectDestroy>();
        Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);

        base.Death();
        LevelManager.Instance.OnEnemyDeath(this);
    }
}