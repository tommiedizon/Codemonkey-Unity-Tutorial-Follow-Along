using UnityEngine;

public class DeliveryCounter : BaseCounter {

    public override void Interact(PlayerMovement player) {

        if (player.HasKitchenObject()) {

            if (player.GetKitchenObject().TryGetPlate(out PlatesKitchenObject plateKitchenObject)) {
                // only accept plates
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                player.GetKitchenObject().DestroySelf();
            }

            
        }
    }
}
