using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{


    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform topOfCounter;

    private KitchenObject kitchenObject;

    public void Interact(PlayerMovement player)
    {

        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, topOfCounter);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {
            // Give object to the player
            kitchenObject.SetKitchenObjectParent(player);
        }

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

    public bool hasKitchenObject()
    {
        return kitchenObject != null;
    }
}

