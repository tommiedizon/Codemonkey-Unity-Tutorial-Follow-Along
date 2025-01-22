using Unity.Collections;
using UnityEngine;

public class ClearCounter : BaseCounter
{


    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(PlayerMovement player) {
        if (!HasKitchenObject()) {
            // Counter is, indeed, clear.
            if (player.HasKitchenObject()) {
                // Player is holding a kitchen object
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }

        } else {
            // There is a KitchenObject on the counter
            if (!player.HasKitchenObject()) {
                this.GetKitchenObject().SetKitchenObjectParent(player);
            } else {
                // Player is holding an object AND there is an object on the counter
                if (player.GetKitchenObject().TryGetPlate(out PlatesKitchenObject platesKitchenObject)) {

                    // Player is holding a plate here
                    if (platesKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                } else {

                    // player is not holding a plate here
                    if (GetKitchenObject().TryGetPlate(out platesKitchenObject)) {
                        // Counter is holding a plate
                        if (platesKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        };
                    }
                }
            }
        }
    }

}
