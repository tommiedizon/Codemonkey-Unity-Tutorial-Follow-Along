using UnityEngine;
using UnityEngine.UI;

public class StoveCounterProgressBarUI : MonoBehaviour {


    [SerializeField] private Image barImage;
    [SerializeField] private StoveCounter stoveCounter;

    private void Start() {
        barImage.fillAmount = 0f;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }
    private void StoveCounter_OnProgressChanged(object sender, StoveCounter.OnProgressChangedEventArgs e) {
        barImage.fillAmount = e.normalizedProgress;
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}