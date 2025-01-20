using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
    [SerializeField] private KitchenObjectSO input;
    [SerializeField] private KitchenObjectSO output;


    public KitchenObjectSO GetKitchenObjectSOInput() { return input; }
    public KitchenObjectSO GetKitchenObjectSOIOutput() { return output; }

    public void SetKitchenObjectSOInput(KitchenObjectSO kitchenObjectSO) { input = kitchenObjectSO; }
    public void SetKitchenObjectSOOutput(KitchenObjectSO kitchenObjectSO) { output = kitchenObjectSO; }
}
