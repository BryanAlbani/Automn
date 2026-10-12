
using System.Collections;
using TMPro;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public float HeatTotal;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private float animationDuration = 0.5f;

    [SerializeField] private GameObject homeButton;
    [SerializeField] private ButtonScript[] allUpgradeButtons;

    private float displayedMoney;
    private Coroutine moneyAnimation;

    void Start()
    {
        HeatTotal = DataManager.Instance.GlobalHeatTotal;
        NewMoney(HeatTotal);

        CheckAllUpgradesPurchased();
    }

    public void UpdateMoney(float valueToAdd)
    {
        DataManager.Instance.UpdateGlobalHeat(valueToAdd);
        NewMoney(HeatTotal + valueToAdd);
        HeatTotal += valueToAdd;
    }

    public void NewMoney(float newMoney)
    {
        if (moneyAnimation != null)
            StopCoroutine(moneyAnimation);

        moneyAnimation = StartCoroutine(AnimateMoney(newMoney));
    }

    private IEnumerator AnimateMoney(float targetMoney)
    {
        float startMoney = displayedMoney;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;

            float percentage = elapsedTime / animationDuration;

            displayedMoney = Mathf.Lerp(startMoney, targetMoney, percentage);

            moneyText.text = Mathf.RoundToInt(displayedMoney).ToString();

            yield return null;
        }

        displayedMoney = targetMoney;
        moneyText.text = Mathf.RoundToInt(displayedMoney).ToString();

        moneyAnimation = null;
    }

    public void CheckAllUpgradesPurchased()
    {
        if (homeButton == null)
            return;

        if (allUpgradeButtons == null || allUpgradeButtons.Length == 0)
        {
            homeButton.SetActive(false);
            return;
        }

        foreach (ButtonScript upgrade in allUpgradeButtons)
        {
            if (upgrade == null ||
                !DataManager.Instance.IsUpgradePurchased(upgrade.UpgradeID))
            {
                homeButton.SetActive(false);
                return;
            }
        }

        homeButton.SetActive(true);
    }
}
