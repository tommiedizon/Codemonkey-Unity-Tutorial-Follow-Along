using UnityEngine;

public class StoveCounterVisual : MonoBehaviour {
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesGameObject;
    [SerializeField] private StoveCounter stoveCounter;
    
    private void Start() {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        bool showVisual = (e.cookedState == StoveCounter.CookedState.Frying) || (e.cookedState == StoveCounter.CookedState.Fried);
        stoveOnGameObject.SetActive(showVisual);
        particlesGameObject.SetActive(showVisual);
    }
}

