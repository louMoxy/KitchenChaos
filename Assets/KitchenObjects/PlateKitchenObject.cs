using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    List<KitchenObjectSO> KitchenObjectSOs;
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSO;

    private void Awake()
    {
        KitchenObjectSOs = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if(validKitchenObjectSO.Contains(kitchenObjectSO))
        {
            if (KitchenObjectSOs.Contains(kitchenObjectSO))
            {
                return false;
            }
            else
            {
                KitchenObjectSOs.Add(kitchenObjectSO);
                OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
                {
                    kitchenObjectSO = kitchenObjectSO
                });

                return true;
            }
        }
        return false;
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList ()
    {
        return KitchenObjectSOs;
    }
}
