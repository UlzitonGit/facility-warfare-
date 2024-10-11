using System.Collections;
using UnityEngine;

public class FloorToDestroy : MonoBehaviour
{
    [SerializeField] Rigidbody[] rb;
    [SerializeField] ParticleSystem part;
    [SerializeField] AudioClip audioClip;
    AudioSource audioSource;
    public int toDestroy = 4;
    bool destroy = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = FindAnyObjectByType<AudioSource>();
        rb = GetComponentsInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(toDestroy == 0 && destroy == false)
        {
            destroy = true;
            part.Play();
            audioSource.PlayOneShot(audioClip);
            for (int i = 0; i < rb.Length; i++)
            {
                
                rb[i].isKinematic = false;
            }
        }
    }
    public void Minus()
    {
        toDestroy -= 1;
    }
   
}
