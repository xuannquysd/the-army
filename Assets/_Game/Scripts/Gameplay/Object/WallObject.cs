using UnityEngine;

public class WallObject : BaseObject
{
    protected override void Death()
    {
        if (GameManager.Instance.GameState != GameState.PLAYING) return;
        //Them effect

        GameObject deathEffectPrefab = EffectManager.Instance.GetEffect<EffectDestroy>();
        Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
        LevelManager.Instance.OnWallDestroyed(this);
    }
}