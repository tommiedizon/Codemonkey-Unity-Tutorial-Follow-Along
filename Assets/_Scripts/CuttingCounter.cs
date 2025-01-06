using UnityEngine;

public class CuttingCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;
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

        }
        else
        {
            // There is a KitchenObject on the counter
            if (!player.hasKitchenObject())
            {
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlt(PlayerMovement player)
    {
        if (hasKitchenObject())
        {
            // There is a Kitchen Object here, cut it!
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
        }
    }
}
