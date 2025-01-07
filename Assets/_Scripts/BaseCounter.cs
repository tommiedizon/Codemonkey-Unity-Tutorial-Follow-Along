using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
   public virtual void Interact(PlayerMovement player)
    {
        Debug.LogError("Basecounter.Interact();");
    }

    public virtual void InteractAlt(PlayerMovement player)
    {
        Debug.LogError("Basecounter.Interact();");
    }

    [SerializeField] private Transform topOfCounter;
    private KitchenObject kitchenObject;

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
