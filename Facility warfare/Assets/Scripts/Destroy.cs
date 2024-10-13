using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Destroying());
    }

    IEnumerator Destroying()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
