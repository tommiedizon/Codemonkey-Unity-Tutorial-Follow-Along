using UnityEngine;

public class SoundManager : MonoBehaviour {

    [SerializeField] private SoundEffectsSO soundEffectsSO;

    public static SoundManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }
    private void Start() {
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        PlayerMovement.OnPickUp += PlayerMovement_OnPickUp;
        BaseCounter.OnObjectPlacedHere += BaseCounter_OnObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e) {
        TrashCounter trashCounter = (TrashCounter)sender;
        PlaySoundFromArray(soundEffectsSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnObjectPlacedHere(object sender, System.EventArgs e) {
        BaseCounter baseCounter = (BaseCounter)sender;
        PlaySoundFromArray(soundEffectsSO.objectDropped, baseCounter.transform.position);
    }

    private void PlayerMovement_OnPickUp(object sender, System.EventArgs e) {
        PlaySoundFromArray(soundEffectsSO.objectPickup, PlayerMovement.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e) {
        //who fired the event?
        CuttingCounter cuttingCounter = (CuttingCounter)sender;
        PlaySoundFromArray(soundEffectsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySoundFromArray(soundEffectsSO.deliveryFailed, deliveryCounter.transform.position); 
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySoundFromArray(soundEffectsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySoundFromArray(AudioClip[] audioClipArray, Vector3 position, float volume = 1f) {
        PlaySound(audioClipArray[UnityEngine.Random.Range(0,audioClipArray.Length)], position, volume);  
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayFootstepsSound(Vector3 position, float volume = 1f) {
        PlaySoundFromArray(soundEffectsSO.footstep, position, volume);
    }
}
