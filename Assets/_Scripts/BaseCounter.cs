using UnityEngine;

public class BaseCounter : MonoBehaviour
{
   public virtual void Interact(PlayerInputActions player)
    {
        Debug.Log("Basecounter.Interact();");
    }
}
