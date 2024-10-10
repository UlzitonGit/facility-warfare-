using UnityEngine;

public class QE : MonoBehaviour
{
    [SerializeField] Transform QEtransform;
    [SerializeField] float qeSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            QEtransform.localRotation = Quaternion.Slerp(QEtransform.localRotation, Quaternion.Euler(0, 0, -20), Time.deltaTime * qeSpeed);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            QEtransform.localRotation = Quaternion.Slerp(QEtransform.localRotation, Quaternion.Euler(0, 0, 20), Time.deltaTime * qeSpeed);
        }
        else QEtransform.localRotation = Quaternion.Slerp(QEtransform.localRotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * qeSpeed);
    }
}
