using System.Runtime.CompilerServices;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    public override void Interact(PlayerMovement player)
    {

        // Logic for picking up and dropping things
        if (!HasKitchenObject())
        {
            // Counter is clear 
            if (player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                // Player is holding a VALID kitchen object
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }

        }
        else
        {
            // There is a KitchenObject on the counter
            if (!player.HasKitchenObject())
            {
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlt(PlayerMovement player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            int currentProgress = GetKitchenObject().GetCuttingProgress();
            int maxProgress = GetKitchenObject().GetMaxCuttingProgress();

            if (currentProgress < maxProgress) // Cutting is not finished yet
            {
                GetKitchenObject().IncrementCuttingProgress();
            } else // cutting is finished
            {
                KitchenObjectSO newKitchenObjectSO = GetKitchenObjectFromRecipe(GetKitchenObject());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(newKitchenObjectSO, this);
            }

        }
    }

    private KitchenObjectSO GetKitchenObjectFromRecipe(KitchenObject kitchenObject)
    {

        KitchenObjectSO kitchenObjectSO = kitchenObject.GetKitchenObjectSO(); 

        for (int i = 0; i < cuttingRecipeSOArray.Length; i++)
        {
            KitchenObjectSO targetKitchenObjectSO = cuttingRecipeSOArray[i].GetKitchenObjectSOInput();
            if (targetKitchenObjectSO == kitchenObjectSO)
            {
                return cuttingRecipeSOArray[i].GetKitchenObjectSOIOutput();
            }
        }

        // Recipe not found
        Debug.Log("Recipe not found.");
        return null;
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if(cuttingRecipeSO.GetKitchenObjectSOInput() == inputKitchenObjectSO)
            {
                return true;
            }
        }

        return false;
    }
}
