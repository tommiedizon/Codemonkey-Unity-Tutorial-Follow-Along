using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private IKitchenObjectParent kitchenObjectParent;

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {

        // check if new parent has kitchenobject
        if (kitchenObjectParent.hasKitchenObject())
        {
            Debug.LogError("KitchenObjectParent already has a KitchenObject");
        }

        // clear the previous parent
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        
        // set new parent
        this.kitchenObjectParent = kitchenObjectParent;

        // inform parent
        kitchenObjectParent.SetKitchenObject(this);

        // update visual
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
        transform.GetChild(0).transform.localPosition = Vector3.zero;
    }
    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }
}
