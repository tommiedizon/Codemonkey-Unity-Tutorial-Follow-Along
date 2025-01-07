using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private CuttingCounter cuttingCounter;

    private void Start()
    {
        barImage.fillAmount = 0f;
        cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;
    }

    private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.currentProgressNormalized;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        float normalizedProgress = (float)cuttingCounter.GetKitchenObject().GetCuttingProgress() / cuttingCounter.GetKitchenObject().GetMaxCuttingProgress();
        Debug.Log(normalizedProgress);
        barImage.fillAmount = normalizedProgress;

    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
