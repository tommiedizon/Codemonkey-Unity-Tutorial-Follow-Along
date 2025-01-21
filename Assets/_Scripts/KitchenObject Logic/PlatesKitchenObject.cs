using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlatesKitchenObject : KitchenObject
{

    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {
        if (kitchenObjectSOList.Contains(kitchenObjectSO)) {
            // already has this kitchenObject
            return false;
        }

        kitchenObjectSOList.Add(kitchenObjectSO);
        return true;
    }
}
