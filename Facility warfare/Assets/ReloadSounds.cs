using UnityEngine;

public class ReloadSounds : MonoBehaviour
{
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip outMag;
    [SerializeField] AudioClip inMag;
    [SerializeField] AudioClip slider;
    [SerializeField] AudioClip magDrop;
    public void MagIN()
    {
        aud.PlayOneShot(inMag);
    }
    public void MagOut()
    {
        aud.PlayOneShot(outMag);
    }
    public void Slider()
    {
        aud.PlayOneShot(slider);
    }
    public void MagDrop()
    {
        aud.PlayOneShot(magDrop);
    }
}
