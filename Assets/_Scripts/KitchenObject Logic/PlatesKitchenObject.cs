using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class PlatesKitchenObject : KitchenObject
{
    public class OnIngredientAddedEventArgs : EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }

    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

    private List<KitchenObjectSO> kitchenObjectSOList;

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {

        if (!validKitchenObjectSOList.Contains(kitchenObjectSO)) {
            // Not a valid KitchenObject
            return false;
        }

        if (kitchenObjectSOList.Contains(kitchenObjectSO)) {
            // already has this kitchenObject
            return false;
        }

        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs {
            kitchenObjectSO = kitchenObjectSO
        });

        kitchenObjectSOList.Add(kitchenObjectSO);
        return true;
    }
}
