using Unity.Collections;
using UnityEngine;

public class ClearCounter : BaseCounter
{


    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(PlayerMovement player)
    {
        if (!hasKitchenObject())
        {
            // Counter is, indeed, clear.
            if (player.hasKitchenObject())
            {
                // Player is holding a kitchen object
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }

        } else
        {
            // There is a KitchenObject on the counter
            if (!player.hasKitchenObject())
            {
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}
