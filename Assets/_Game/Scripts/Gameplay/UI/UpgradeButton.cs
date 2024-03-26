using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] Button btn;
    [SerializeField] TMP_Text statisticText, priceText;

    public void AddListener(UnityAction callback)
    {
        btn.onClick.AddListener(callback);
    }

    public void UpdateUI(float statistic, int price)
    {
        statisticText.text = statistic.ToString();
        priceText.text = "$" + price;
    }
}