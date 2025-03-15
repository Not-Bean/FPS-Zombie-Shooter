using UnityEngine;
using FMODUnity;

public class UIButtonSound : MonoBehaviour
{
    public void PlayHoverSound()
    {
            RuntimeManager.PlayOneShot(FMODEvents.instance.UIButtonHover, Vector3.zero);
    }

    public void PlayClickSound()
    {
        RuntimeManager.PlayOneShot(FMODEvents.instance.UIButtonClick, Vector3.zero);
    }
}