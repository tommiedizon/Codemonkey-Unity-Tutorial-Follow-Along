using UnityEngine;

public class DeliveryCounter : BaseCounter {

    public static DeliveryCounter Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }
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
