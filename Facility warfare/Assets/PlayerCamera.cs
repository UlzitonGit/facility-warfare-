
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;
    float xRotation;
    float yRotation;
    private Vector3 currentRotation;
    private Vector3 targetRotation;
    [SerializeField] Weapon weapon;
    int mult = 1;

    [SerializeField] private float recoilX;
    [SerializeField] private float recoilY;
    [SerializeField] private float recoilZ;

    [SerializeField] private float snappiness;
    [SerializeField] private float returnSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon.isAiming == true) mult = 2;
        if (weapon.isAiming == false) mult = 1;
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation / mult, snappiness * Time.deltaTime);
       

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        transform.rotation = Quaternion.Euler( new Vector3 (xRotation, yRotation, 0) + currentRotation);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
    public void RecoilFire()
    {
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }
}
