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
                if(player.GetKitchenObject().TryGetPlate(out PlatesKitchenObject plateKitchenObject)) {
                    
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }

                }
            }
        }
    }

}
