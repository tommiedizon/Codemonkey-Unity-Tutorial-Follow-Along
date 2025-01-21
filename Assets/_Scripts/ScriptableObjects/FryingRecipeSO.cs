using UnityEngine;

[CreateAssetMenu(fileName = "FryingObjectSO", menuName = "Scriptable Objects/FryingObjectSO")]
public class FryingRecipeSO : ScriptableObject
{
    [SerializeField] KitchenObjectSO input;
    [SerializeField] KitchenObjectSO output;
    [SerializeField] float maxFryingTime;

    public KitchenObjectSO GetFryingObjectSOInput() { return input; }
    public KitchenObjectSO GetFryingObjectSOIOutput() { return output; }
    public float GetMaxFryingTime() { return maxFryingTime; }
}
 