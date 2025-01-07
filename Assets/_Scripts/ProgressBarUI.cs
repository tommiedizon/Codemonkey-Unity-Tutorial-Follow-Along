using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private CuttingCounter cuttingCounter;

    private void Start()
    {
        barImage.fillAmount = 0f;
        cuttingCounter.GetKitchenObject().OnProgressChanged += ProgressBarUI_OnProgressChanged;
    }

    private void ProgressBarUI_OnProgressChanged(object sender, KitchenObject.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = (float)e.currentCuttingProgress/e.currentKitchenObjectSO.maxCuttingProgress;
    }
}
