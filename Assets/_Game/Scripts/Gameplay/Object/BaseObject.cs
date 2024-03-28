using TMPro;
using UnityEngine;

public abstract class BaseObject : MonoBehaviour
{
    [SerializeField] private float rawHP, rawDefend;
    [SerializeField] private TMP_Text hpTxt;

    private float rawHPReact;
    private float currentHP = 0;

    public TMP_Text HpTxt { get => hpTxt; set => hpTxt = value; }
    public float CurrentHP {
        get => currentHP; 
        set {
            currentHP = value;
            HpTxt.text = Mathf.CeilToInt(CurrentHP).ToString();
        } 
    }
    public float RawHP { get => rawHP; set => rawHP = value; }
    public float RawDefend { get => rawDefend; set => rawDefend = value; }
    protected float RawHPReact { get => rawHPReact; set => rawHPReact = value; }

    protected void Start()
    {
        if (rawHPReact < rawHP)
        {
            rawHPReact = rawHP;
            CurrentHP += RawHPReact;
        }
    }

    public virtual void GetDamage(float value, CharacterObject objectSendDamage)
    {
        CurrentHP -= (value - RawDefend);

        if (CurrentHP > 0) return;
        objectSendDamage.OnKillTarget();
        Death();
    }

    protected abstract void Death();
}
