using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwayAbdBob : MonoBehaviour
{
    [SerializeField] FirstPersonController firstPersonController;
    [SerializeField] Rigidbody rb;
    Vector3 localPos;
    float multipl;
    [SerializeField] Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        localPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon.isAiming == true) multipl = 2f;
        else multipl = 1f;
        GetInput();
        Sway();
        SwayRotation();
        SwayRotationMove();
        BobOffset();
        CompositePositionRotation();
        SwayPosMove();
    }
    Vector2 walkInput;
    Vector2 lookInput;
    float Yspeed = 0;
    void GetInput()
    {
        walkInput.x = Input.GetAxis("Horizontal");
        walkInput.y = Input.GetAxis("Vertical");
        walkInput = walkInput.normalized;

        lookInput.x = Input.GetAxis("Mouse X");
        lookInput.y = Input.GetAxis("Mouse Y"); 
        if(rb != null) Yspeed = rb.linearVelocity.y;


    }

    public float step = 0.01f;
    public float maxStepDist = 0.06f;
    Vector3 swayPos;
    void Sway()
    {
        Vector3 invertLook = lookInput * -step;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxStepDist, maxStepDist);
        invertLook.y = Mathf.Clamp(invertLook.y, -maxStepDist, maxStepDist);

        swayPos = invertLook;
    }

    public float rotSpeed = 4f;
    public float maxRotSpeed = 5f;
    Vector3 swayEulerRot;
    void SwayRotation()
    {
        Vector2 invertLook = lookInput * -rotSpeed;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxRotSpeed, maxRotSpeed);
        invertLook.y = Mathf.Clamp(invertLook.y, -maxRotSpeed, maxRotSpeed);

        swayEulerRot = new Vector3(invertLook.y, invertLook.x, invertLook.x);
    }
    public float moveRotSpeed = 4f;
    public float moveMaxRotSpeed = 5f;
    Vector3 walkSwayEulerRot;
    void SwayRotationMove()
    {
        Vector2 invertLook = walkInput * -moveRotSpeed;
        invertLook.x = Mathf.Clamp(invertLook.x, -moveMaxRotSpeed, moveMaxRotSpeed);
        if(Yspeed > 4 ) Yspeed = 4;
        if (Yspeed < -4) Yspeed = -4;
        invertLook.y = Yspeed;

        walkSwayEulerRot = new Vector3(invertLook.y, 0, invertLook.x);
    }
    public float movePosSpeed = 0.2f;
    public float moveMaxPosSpeed = 5f;
    Vector3 walkSwayPos;
    void SwayPosMove()
    {
        Vector2 invertLook = walkInput * -movePosSpeed;
        invertLook.x = Mathf.Clamp(invertLook.x, -moveMaxPosSpeed, moveMaxPosSpeed);
        invertLook.y = Mathf.Clamp(invertLook.y, -moveMaxPosSpeed, moveMaxPosSpeed);

      
        walkSwayPos = new Vector3(invertLook.x, invertLook.y, invertLook.x);
    }

    public float smooth = 10f;
    public float smoothRot = 12f;
    public float smoothWalkRot = 12f;
    public float smoothWalkPos = 12f;

    public float speedCurve;
    float curveSin { get => Mathf.Sin(speedCurve); }
    float curveCos { get => Mathf.Cos(speedCurve); }
    public Vector3 travelLimit = Vector3.one * 0.025f;
    public Vector3 bobLimit = Vector3.one * 0.01f;
    Vector3 bobPosition;

    void BobOffset()
    {

        speedCurve += Time.deltaTime * (firstPersonController.isGrounded == true ? rb.linearVelocity.magnitude : 1f) + 0.01f;
        if(rb.linearVelocity.magnitude == 0) return;
        bobPosition.x = (curveCos * bobLimit.x * (firstPersonController.isGrounded == true ? 1 : 0)) - (walkInput.x * travelLimit.x);
        bobPosition.y = (curveSin * bobLimit.y * (firstPersonController.isGrounded == true ? 1 : 0)) - (walkInput.y * travelLimit.y);
        //bobPosition.z = (walkInput.y - travelLimit.z);
    }
    public Vector3 mult;
    Vector3 bobEulerRotation;
   
    void CompositePositionRotation()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, swayPos + bobPosition, smooth * Time.deltaTime);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(swayEulerRot / multipl) , smoothRot * Time.deltaTime);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(walkSwayEulerRot / multipl), smoothWalkRot * Time.deltaTime);
        
        if(walkSwayPos != Vector3.zero) transform.localPosition = Vector3.Lerp(transform.localPosition, walkSwayPos , smoothWalkPos * Time.deltaTime);
        if (walkSwayPos == Vector3.zero) transform.localPosition = Vector3.Lerp(transform.localPosition, localPos , 5 * Time.deltaTime);
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, 0), smooth * (multipl * multipl) * Time.deltaTime);

    }
}
