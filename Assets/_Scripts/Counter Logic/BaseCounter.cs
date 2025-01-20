using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform topOfCounter;
    private KitchenObject kitchenObject;

    public virtual void Interact(PlayerMovement player)
    {
        Debug.LogError("Basecounter.Interact();");
    }

    public virtual void InteractAlt(PlayerMovement player)
    {
        Debug.LogError("Basecounter.Interact();");
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return topOfCounter;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() { return kitchenObject; }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
