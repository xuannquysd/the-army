using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterObject : BaseObject
{
    [SerializeField] private float rawDamage;
    [SerializeField] private float rawSpeedMove, rawSpeedRotate, rawSpeedAttack;
    [SerializeField] private float rawRadiusAttack;
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private Animator animator;

    protected BaseObject currentTarget;
    protected List<Transform> allTargetObject;
    protected int totalTarget;
    protected Vector2 dirMove;

    #region Statistic React
    protected float damage;
    protected float speedMove, speedAttack;
    protected float radiusAttack;
    #endregion

    public float RawDamage { get => rawDamage; set => rawDamage = value; }
    public float RawSpeedMove { get => rawSpeedMove; set => rawSpeedMove = value; }
    public float RawSpeedRotate { get => rawSpeedRotate; set => rawSpeedRotate = value; }
    public float RawSpeedAttack { get => rawSpeedAttack; set => rawSpeedAttack = value; }
    public float RawRadiusAttack { get => rawRadiusAttack; set => rawRadiusAttack = value; }

    protected void Start()
    {
        base.Start();
        InitStatisticReact();
        transform.DOScale(1f, 0.5f).From(0f).SetEase(Ease.OutBack);
    }

    void InitStatisticReact()
    {
        damage = RawDamage;
        speedMove = RawSpeedMove;
        speedAttack = RawSpeedAttack;
        radiusAttack = RawRadiusAttack;
    }

    public void InitTargetObject(List<Transform> allTargetObject)
    {
        this.allTargetObject = allTargetObject;
        totalTarget = this.allTargetObject.Count;

        FindNearTarget();
    }

    protected void FindNearTarget()
    {
        if (allTargetObject == null || totalTarget == 0) return;

        BaseObject foundTarget = GetNearTarget();
        currentTarget = foundTarget ? foundTarget : currentTarget;
    }

    protected BaseObject GetNearTarget()
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
        if (GameManager.Instance.GameState != GameState.PLAYING) return;
        Destroy(gameObject);
    }

    protected virtual void Attack()
    {
        if(GameManager.Instance.GameState != GameState.PLAYING) return;

        if (currentTarget == null) return;
        currentTarget.GetDamage(damage, this);
    }

    public virtual void OnKillTarget()
    {
        FindNearTarget();
    }

    protected virtual void FollowTarget()
    {
        if (currentTarget == null) return;
        dirMove = currentTarget.transform.position - transform.position;

        RotToTarget();
        MoveToTarget();
    }

    void MoveToTarget()
    {
        float distance = Vector2.Distance(currentTarget.transform.position, transform.position);
        if (distance > radiusAttack)
        {
            rigid.velocity = speedMove * Time.deltaTime * dirMove.normalized;
            animator.speed = speedAttack;
            animator.SetBool("Attacking", false);
        }
        else
        {
            rigid.velocity = Vector2.zero;
            animator.SetBool("Attacking", true);
        }
    }

    void RotToTarget()
    {
        float angle = Mathf.Atan2(dirMove.y, dirMove.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * RawSpeedRotate);
    }

    public void StopAttack()
    {
        animator.SetBool("Attacking", false);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RawRadiusAttack);
    }
#endif

    public void Progress()
    {
        FollowTarget();
        FindNearTarget();

        HpTxt.rectTransform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles * -1f);
    }
}