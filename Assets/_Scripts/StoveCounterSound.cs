using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour {

    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        bool playSound = e.cookedState == StoveCounter.CookedState.Frying || e.cookedState == StoveCounter.CookedState.Fried;
        if (playSound) {
            audioSource.Play();
        } else {
            audioSource.Stop();
        }
    }
}
