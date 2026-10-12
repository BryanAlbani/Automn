using System.Collections;
using TMPro;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public float HeatTotal;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private float animationDuration = 0.5f;

    void Start()
    {
        HeatTotal = DataManager.Instance.GlobalHeatTotal;
        NewMoney(HeatTotal);
    }

    private float displayedMoney;
    private Coroutine moneyAnimation;

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

}
