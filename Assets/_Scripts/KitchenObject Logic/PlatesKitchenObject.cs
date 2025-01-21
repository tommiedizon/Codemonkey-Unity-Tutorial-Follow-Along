using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlatesKitchenObject : KitchenObject
{

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

        kitchenObjectSOList.Add(kitchenObjectSO);
        return true;
    }
}
