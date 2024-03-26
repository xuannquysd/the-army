using UnityEngine;

public class InfoArmyUI : MonoBehaviour
{
    [SerializeField] UpgradeButton buyUnitBtn, upgradePowersBtn, upgradeHealthBtn, upgradeAtkSpeedBtn;
    [SerializeField] AllyObject armyPrefab;

    [HideInInspector] public AllyType Type;

    private void Awake()
    {
        buyUnitBtn.AddListener(OnClickBuyUnit);
    }

    void OnClickBuyUnit()
    {
        Vector3 posSpawn = new(0f, 2f, 0f);
        AllyObject army = Instantiate(armyPrefab, posSpawn, Quaternion.identity);
        LevelManager.Instance.OnChangeQuantityAlly(army);
    }

    void InitObserver()
    {

    }

    void UpdateUIUpgradeBtn(object data)
    {
        
    }
}