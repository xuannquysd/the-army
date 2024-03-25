using TMPro;
using UnityEngine;

public abstract class BaseObject : MonoBehaviour
{
    [SerializeField] private float hp;
    [SerializeField] private TMP_Text hpTxt;

    protected float Hp { get => hp; set => hp = value; }

    protected void Start()
    {
        hpTxt.text = hp.ToString();
    }

    public void GetDamage(int value)
    {
        hp -= value;
        hpTxt.text = hp.ToString();

        if (hp > 0) return;
        Death();
    }

    protected abstract void Death();
}
