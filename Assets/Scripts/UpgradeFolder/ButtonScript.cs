using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string Description;
    public float valueCost;
    public float effectValue;

    public GameObject[] NextUpgrade;
    public Button SelfButton;

    public TextMeshProUGUI DescriptionText;
    public TextMeshProUGUI CostText;
    public TextMeshProUGUI EffectText;

    private bool isActive = true;


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
            UpgradeManager.Instance.UpdateMoney(-valueCost);
            //Mettre à jour les valeurs
            SelfButton.interactable = false;
            foreach (GameObject upgrade in NextUpgrade)
            {
                if (upgrade != null)
                {
                    upgrade.SetActive(true);
                }
            }
        }
        else
            {
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
    }
