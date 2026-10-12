using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string Description;
    public float valueCost;
    public float effectValue;
    public string UpgradeID;

    public GameObject[] NextUpgrade;
    public Button SelfButton;

    public TextMeshProUGUI DescriptionText;
    public TextMeshProUGUI CostText;
    public TextMeshProUGUI EffectText;

    public enum UpgradeType
    {
        HeatValue,
        FallSpeed,
        SpawnInterval,
        SpawnDuration,
        BarrelSpeed,
        BarrelScale,
        Nothing
    }

    public UpgradeType upgradeType;

    private void Start()
    {
        // Restaurer les améliorations déjà achetées
        if (DataManager.Instance.IsUpgradePurchased(UpgradeID))
        {
            SelfButton.interactable = false;
        }

        if (DataManager.Instance.IsUpgradePurchased(UpgradeID))
        {
            foreach (GameObject upgrade in NextUpgrade)
            {
                if (upgrade != null)
                    upgrade.SetActive(true);
            }
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DescriptionText.text = Description;
        CostText.text = "Cost : " + valueCost;
        EffectText.text = "Effect : +" + effectValue;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DescriptionText.text = "";
        CostText.text = "";
        EffectText.text = "";
    }

    public void OnButtonClic()
    {

        if (valueCost <= UpgradeManager.Instance.HeatTotal)
        {
            AudioManager.Instance.PlayClic();
            AudioManager.Instance.PlaySFX(AudioManager.Instance.Cash);
            UpgradeManager.Instance.UpdateMoney(-valueCost);
            ApplyUpgrade();
            SelfButton.interactable = false;
            DataManager.Instance.PurchaseUpgrade(UpgradeID);

            foreach (GameObject upgrade in NextUpgrade)
            {
                if (upgrade != null)
                {
                    upgrade.SetActive(true);
                }
            }
            UpgradeManager.Instance.CheckAllUpgradesPurchased();
        }
        else
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.Error);
            print("ceb c'est déja fait");
        }
        /*if(value > UpgradeManager.Instance.HeatTotal)
            {
            /Play sound error
            //Maybe un lessage pas assez de thune
            }
        else
        {
            //Play sound achat/upgrade
            //Destroy bouton
            //Retirer la valeur du cout et update l'affichage
            //Effectuer l'upgrade
        }
        */

    }

    public void OnButtonFinal()
    {
        if (valueCost <= UpgradeManager.Instance.HeatTotal)
        {
            AudioManager.Instance.PlayClic();
            AudioManager.Instance.PlaySFX(AudioManager.Instance.Cash);

            SceneManager.LoadScene("EndScene");
        }
        else
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.Error);
        }
    }


    private void ApplyUpgrade()
    {
        DataManager data = DataManager.Instance;

        switch (upgradeType)
        {
            case UpgradeType.HeatValue:
                data.valueHeat += effectValue;
                break;

            case UpgradeType.FallSpeed:
                data.bonusFallSpeed += effectValue;
                break;

            case UpgradeType.SpawnInterval:
                data.timeBetweenSpawns = Mathf.Max(0.01f, data.timeBetweenSpawns - effectValue);
                break;

            case UpgradeType.SpawnDuration:
                data.maxSpawnDuration += effectValue;
                break;

            case UpgradeType.BarrelSpeed:
                data.SpeedBarrel += effectValue;
                break;

            case UpgradeType.BarrelScale:
                data.ScaleBarrel += effectValue;
                break;
            case UpgradeType.Nothing:
                break;
        }
    }



}
