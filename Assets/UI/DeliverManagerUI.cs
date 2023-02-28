using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverManagerUI : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    void UpdateVisual()
    {
        foreach(Transform child in container)
        {
            if(child != recipeTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        foreach(RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipes()) {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManageSingleUI>().SetRecipeSo(recipeSO);
        }
    }
}
