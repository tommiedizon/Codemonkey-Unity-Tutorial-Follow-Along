using UnityEngine;
using System.Collections.Generic;
using System;

public class DeliveryManager : MonoBehaviour {

    public static DeliveryManager Instance { get; private set; }

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;
    private int recipeSuccessAmount = 0;

    private void Awake() {
        waitingRecipeSOList = new List<RecipeSO>();
        Instance = this;
    }

    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;

        if(spawnRecipeTimer <= 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if (waitingRecipeSOList.Count < waitingRecipesMax) {
                RecipeSO recipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(recipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }


    }

    public void DeliverRecipe(PlatesKitchenObject plateKitchenObject) {
        for(int i = 0; i< waitingRecipeSOList.Count; i++) {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            
            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {
                // Has the same number of ingredients
                bool plateContentMatchesRecipe = true;
                foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) {
                    // Cycling through all ingredients in the recipe
                    bool ingredientFound = false;
                    foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
                        // Cycling through ingredients on the plate
                        if(plateKitchenObjectSO == recipeKitchenObjectSO) {
                            // Ingredients match
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound) {
                        // Recipe ingredient not found on the plate
                        plateContentMatchesRecipe = false;
                    }

                }

                if (plateContentMatchesRecipe) {
                    // Player delivered the correct recipe
                    waitingRecipeSOList.RemoveAt(i);
                    recipeSuccessAmount++;
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        // No match is found
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);

    }

    public int GetRecipeSuccessAmount() {
        return recipeSuccessAmount;
    }
    public List<RecipeSO> GetWaitingRecipeSOList() { return waitingRecipeSOList; }
}
