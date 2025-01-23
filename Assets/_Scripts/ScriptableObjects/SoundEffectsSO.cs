using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SoundEffectsSO", menuName = "Scriptable Objects/SoundEffectsSO")]
public class SoundEffectsSO : ScriptableObject {
    public AudioClip[] chop;
    public AudioClip[] deliveryFailed;
    public AudioClip[] deliverySuccess;
    public AudioClip[] footstep;
    public AudioClip[] objectDropped;
    public AudioClip[] objectPickup;
    public AudioClip stoveSizzle;
    public AudioClip[] trash;
    public AudioClip[] warning;
}
