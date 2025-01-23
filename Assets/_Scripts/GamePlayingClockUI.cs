using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour {
    [SerializeField] Image timerImage;
    private void Update() {
        if (GameManager.Instance.IsGamePlaying()) {
            timerImage.fillAmount = GameManager.Instance.GetNormalizedTimeRemaining();
        }
    }
}
