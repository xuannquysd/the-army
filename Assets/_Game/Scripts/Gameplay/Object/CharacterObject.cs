using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterObject : BaseObject
{
    [SerializeField] private float damage;
    [SerializeField] private float speedMove, speedAttack;
    [SerializeField] private float radiusAttack;
    [SerializeField] private Rigidbody2D rigid;

    protected List<Transform> allTargetObject;
    protected BaseObject currentTarget;
    protected int totalTarget;

    public float Damage { get => damage; set => damage = value; }
    public float SpeedMove { get => speedMove; set => speedMove = value; }
    public float SpeedAttack { get => speedAttack; set => speedAttack = value; }
    public float RadiusAttack { get => radiusAttack; set => radiusAttack = value; }
    protected Rigidbody2D Rigid { get => rigid; set => rigid = value; }

    protected void Start()
    {
        base.Start();
    }

    public void InitTargetObject(List<Transform> allTargetObject)
    {
        this.allTargetObject = allTargetObject;
        totalTarget = this.allTargetObject.Count;
        currentTarget = FindNearTarget();
    }

    protected BaseObject FindNearTarget()
    {
        float nearTarget = 9999f;
        int indexTarget = 0;
        Vector2 selfPosition = transform.position;
        for(int i =0; i < totalTarget; i++)
        {
            if (allTargetObject[i] == null) continue;
            float distance = Vector2.Distance(selfPosition, allTargetObject[i].position);
            if(distance < nearTarget)
            {
                nearTarget = distance;
                indexTarget = i;
            }
        }

        if (allTargetObject[indexTarget] == null) return null;
        else return allTargetObject[indexTarget].GetComponent<BaseObject>();
    }

    protected override void Death()
    {
        Destroy(gameObject);
    }

    protected abstract void Attack();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RadiusAttack);
    }
}