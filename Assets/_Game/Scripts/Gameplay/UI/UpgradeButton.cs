using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] Button btn;
    [SerializeField] TMP_Text statisticText, priceText;

    int _price;

    public void AddListener(UnityAction callback)
    {
        btn.onClick.AddListener(callback);
    }

    public void UpdateUI(float statistic, int price)
    {
        _price = price;

        statisticText.text = statistic.ToString();
        priceText.text = "$" + price;
    }

    public void UpdateColor()
    {
        priceText.color = _price > SessionPref.GetCurrentBattleMoney() ? Color.red : Color.white;
    }

    public void UpdateFixedColor(Color color)
    {
        priceText.color = color;
    }
}