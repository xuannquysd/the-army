public class WallObject : BaseObject
{
    protected override void Death()
    {
        //Them effect

        Destroy(gameObject);
        LevelManager.Instance.OnWallDestroyed(this);
    }
}