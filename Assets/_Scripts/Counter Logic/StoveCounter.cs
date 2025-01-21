using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private StoveCounterProgressBarUI stoveCounterProgressBarUI;
    private FryingRecipeSO fryingRecipeSO;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs {
        public CookedState cookedState;
    }

    public class OnProgressChangedEventArgs : EventArgs {
        public float normalizedProgress;

        public OnProgressChangedEventArgs(float normalizedProgress) {
            this.normalizedProgress = normalizedProgress;
        }
    }

    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;

    /* 
     In my opinion, it's not ideal for the StoveCounter to store the state of the meat
     matty. The meat patty should store its own state, maybe just inherit from KitchenObject
     and move the state logic to a MeatPatty class. For now though, I am happy with this. 
     */
    public enum CookedState {
        Idle,
        Frying,
        Fried,
        Burnt
    }

    private CookedState cookedState;

    private float fryingTimer;

    private void Start() {
        cookedState = CookedState.Idle;
    }

    float normalizedProgress;

    private void UpdateProgressBarUI() {
        normalizedProgress = (float)fryingTimer / fryingRecipeSO.GetMaxFryingTime();
        OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs(normalizedProgress));
    }

    private void UpdateState(CookedState cookedState) {
        this.cookedState = cookedState;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
            cookedState = cookedState
        });
    }

    private void Update() {

        if (HasKitchenObject()) {
            switch (cookedState) {
                case CookedState.Idle:
                    break;
                case CookedState.Frying:
                    stoveCounterProgressBarUI.Show();
                    fryingTimer += Time.deltaTime;
                    fryingRecipeSO = GetFryingRecipeSOFromKitchenObject(GetKitchenObject());
                    UpdateProgressBarUI();

                    if (fryingTimer > fryingRecipeSO.GetMaxFryingTime()) {
                        // Fried
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.GetFryingObjectSOIOutput(), this);
                        fryingTimer = 0f;
                        UpdateState(CookedState.Fried);
                    }                             
                    break;
                case CookedState.Fried:
                    fryingTimer += Time.deltaTime;
                    UpdateProgressBarUI();
                    fryingRecipeSO = GetFryingRecipeSOFromKitchenObject(GetKitchenObject());
                    if (fryingTimer > fryingRecipeSO.GetMaxFryingTime()) {
                        // Burnt
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.GetFryingObjectSOIOutput(), this);
                        UpdateState(CookedState.Burnt);
                    }
                    break;
                case CookedState.Burnt:
                    Debug.Log("Burnt");
                    stoveCounterProgressBarUI.Hide();
                    break;
            }
        }
    }

    public override void Interact(PlayerMovement player) {

        // Logic for picking up and dropping things
        if (!HasKitchenObject()) {
            // Counter is clear 
            if (player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                // Player is holding a VALID kitchen object
                player.GetKitchenObject().SetKitchenObjectParent(this);
                UpdateState(CookedState.Frying);
            }

        } else {
            // There is a KitchenObject on the counter
            if (!player.HasKitchenObject()) {
                this.GetKitchenObject().SetKitchenObjectParent(player);
                UpdateState(CookedState.Idle);
                stoveCounterProgressBarUI.Hide();

            }
        }
    }

    private KitchenObjectSO GetOutputKitchenObjectSOFromRecipe(KitchenObject kitchenObject) {

        KitchenObjectSO kitchenObjectSO = kitchenObject.GetKitchenObjectSO();

        for (int i = 0; i < fryingRecipeSOArray.Length; i++) {
            KitchenObjectSO targetKitchenObjectSO = fryingRecipeSOArray[i].GetFryingObjectSOInput();
            if (targetKitchenObjectSO == kitchenObjectSO) {
                Debug.Log(targetKitchenObjectSO);
                return fryingRecipeSOArray[i].GetFryingObjectSOIOutput();
            }
        }

        // Recipe not found
        Debug.Log("Recipe not found.");
        return null;
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray) {
            if (fryingRecipeSO.GetFryingObjectSOInput() == inputKitchenObjectSO) {
                return true;
            }
        }

        return false;
    }

    private FryingRecipeSO GetFryingRecipeSOFromKitchenObject(KitchenObject kitchenObject) {

        KitchenObjectSO kitchenObjectSO = kitchenObject.GetKitchenObjectSO();
        
        if (kitchenObjectSO == null) { return null; }

        foreach(FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray) {
            if (fryingRecipeSO.GetFryingObjectSOInput() == kitchenObjectSO) {
                return fryingRecipeSO;
            }
        }

        return null;
    }

}
