using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private IKitchenObjectParent kitchenObjectParent;
    private int cuttingProgress = 0;

    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float currentCuttingProgress;
        public KitchenObjectSO currentKitchenObjectSO;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {

        // check if new parent has kitchenobject
        if (kitchenObjectParent.HasKitchenObject())
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


    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }

    public int GetCuttingProgress()
    {
        return cuttingProgress;
    }
   
    public int GetMaxCuttingProgress()
    {
        return GetKitchenObjectSO().maxCuttingProgress;
    }
    
    public void IncrementCuttingProgress()
    {
        cuttingProgress += 1;
        OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs { 
            currentCuttingProgress = cuttingProgress, currentKitchenObjectSO = kitchenObjectSO
        });
    }
}
