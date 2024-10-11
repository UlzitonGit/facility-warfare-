using UnityEngine;

public class Destroyable : MonoBehaviour
{
    [SerializeField] FloorToDestroy parent;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Destory()
    {
        parent.Minus();
        rb.isKinematic = false;
    }
}
