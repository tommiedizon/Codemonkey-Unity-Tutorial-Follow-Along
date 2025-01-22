using System.Runtime.CompilerServices;
using UnityEngine;
using System;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    [SerializeField] private ProgressBarUI progressBarUI;

    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;

    public class OnProgressChangedEventArgs : EventArgs
    {
        public float currentProgressNormalized;

    }

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

                // Enable the progress bar UI
                progressBarUI.Show();
            }

        }
        else
        {
            // There is a KitchenObject on the counter
            if (!player.HasKitchenObject())
            {
                this.GetKitchenObject().SetKitchenObjectParent(player);

                //Disable the progress bar UI
                progressBarUI.Hide();
                
            }
        }
    }

    public override void InteractAlt(PlayerMovement player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            GetKitchenObject().IncrementCuttingProgress();
            OnCut?.Invoke(this, EventArgs.Empty);

            int currentProgress = GetKitchenObject().GetCuttingProgress();
            int maxProgress = GetKitchenObject().GetMaxCuttingProgress();

            if (currentProgress < maxProgress) // Cutting is not finished yet
            {
                // update the UI
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs {
                    currentProgressNormalized = (float)currentProgress / (float)maxProgress
                });

            } else // cutting is finished
            {
                progressBarUI.Hide();
                KitchenObjectSO newKitchenObjectSO = GetKitchenObjectSOFromRecipe(GetKitchenObject());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(newKitchenObjectSO, this);

                //update the UI
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs {
                    currentProgressNormalized = 1
                });

            }

        }
    }

    private KitchenObjectSO GetKitchenObjectSOFromRecipe(KitchenObject kitchenObject)
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
