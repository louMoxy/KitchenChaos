using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeliveryManageSingleUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recipeNameText;
    [SerializeField] Transform iconContainer;
    [SerializeField] Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSo(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;

        foreach(Transform child in iconContainer)
        {
            if(child != iconTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        foreach(KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOs)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}
