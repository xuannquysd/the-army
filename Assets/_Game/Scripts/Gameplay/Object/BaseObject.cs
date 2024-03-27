using TMPro;
using UnityEngine;

public abstract class BaseObject : MonoBehaviour
{
    [SerializeField] private float rawHP, rawDefend;
    [SerializeField] private TMP_Text hpTxt;

    private float currentHP;
    public TMP_Text HpTxt { get => hpTxt; set => hpTxt = value; }
    public float CurrentHP { get => currentHP; set => currentHP = value; }
    public float RawHP { get => rawHP; set => rawHP = value; }

    protected void Start()
    {
        CurrentHP = RawHP;
        HpTxt.text = CurrentHP.ToString();
    }

    public virtual void GetDamage(float value, CharacterObject objectSendDamage)
    {
        CurrentHP -= (value - rawDefend);
        HpTxt.text = Mathf.CeilToInt(CurrentHP).ToString();

        if (CurrentHP > 0) return;
        objectSendDamage.OnKillTarget();
        Death();
    }

    protected abstract void Death();
}
