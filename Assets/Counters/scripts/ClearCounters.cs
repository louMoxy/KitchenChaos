using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounters : BaseCounter
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if(!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        } else
        {
            if(player.HasKitchenObject())
            {
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // Player is holding a plate
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                } else
                {
                    // Player is not carrying plate but something else
                    if (GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject1))
                    {
                        // counter is holding a plate
                        if (plateKitchenObject1.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            } else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
