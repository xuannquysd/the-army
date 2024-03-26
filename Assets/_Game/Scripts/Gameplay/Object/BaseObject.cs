using TMPro;
using UnityEngine;

public abstract class BaseObject : MonoBehaviour
{
    [SerializeField] private float rawHP, rawDefend;
    [SerializeField] private TMP_Text hpTxt;

    protected float currentHP;

    public float RawHp { get => rawHP; set => rawHP = value; }
    public TMP_Text HpTxt { get => hpTxt; set => hpTxt = value; }

    protected void Start()
    {
        currentHP = rawHP;
        HpTxt.text = currentHP.ToString();
    }

    public void GetDamage(float value)
    {
        currentHP -= value - rawDefend;
        HpTxt.text = Mathf.CeilToInt(currentHP).ToString();

        if (currentHP > 0) return;
        Death();
    }

    protected abstract void Death();
}
