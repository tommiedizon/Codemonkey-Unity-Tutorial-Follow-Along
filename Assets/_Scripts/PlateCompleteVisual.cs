using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class PlateCompleteVisual : MonoBehaviour
{

    // The purpose of this struct is to establish a link between
    // GameObjects in the unity editor and KitchenObjectSO's

    [Serializable]
    public struct KitchenObjectSO_GameObject {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlatesKitchenObject platesKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;

    private void Start() {
        platesKitchenObject.OnIngredientAdded += PlatesKitchenObject_OnIngredientAdded;

        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSOGameObjectList) {
            kitchenObjectSO_GameObject.gameObject.SetActive(false);
        }

    }

    private void PlatesKitchenObject_OnIngredientAdded(object sender, PlatesKitchenObject.OnIngredientAddedEventArgs e) {
        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSOGameObjectList) {
            if(kitchenObjectSO_GameObject.kitchenObjectSO == e.kitchenObjectSO) {
                kitchenObjectSO_GameObject.gameObject.SetActive(true);
            }
        }
    }
}
