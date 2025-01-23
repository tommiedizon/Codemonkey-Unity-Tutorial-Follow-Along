using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour {

    [SerializeField] TextMeshProUGUI recipesDeliveredText;

    private void Start() {
        Hide();
        GameManager.Instance.OnStateChanged += Instance_OnStateChanged;
    }

    private void Instance_OnStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsGameOver()) {
            Show();
            recipesDeliveredText.text = DeliveryManager.Instance.GetRecipeSuccessAmount().ToString();
        } else {
            Hide();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
