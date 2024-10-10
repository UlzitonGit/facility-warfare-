using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region Variables
    [SerializeField] private float delayBetweenShots = 0.1f;
    [SerializeField] private float reloadTime = 1f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private int maxAmmo = 30;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private KeyCode shootKey = KeyCode.Mouse0;
    [SerializeField] private ParticleSystem muzzleFireEffect;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private Animator weaponAnimation;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] float aimSmooth = 0.000000000001f;
    private int ammo = 0;
    private AudioSource aud;
    public bool isAiming = false;
    public bool isSemiAiming = false;
    bool isReloading = false;
    private bool canShoot = true;
    [SerializeField] Recoil recoilScriptCamera;
    [SerializeField] Recoil recoilScriptWeapon;
   // [SerializeField] Vector3 aimRot;
    [SerializeField] Vector3 aimPos;
    [SerializeField] Vector3 semiAimPos;
    [SerializeField] Quaternion semiAimRot;
    [SerializeField] Vector3 armPos;
    bool aimSwitch = true;
   
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        ammo = maxAmmo;
     
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse2) && aimSwitch)
        {
            isSemiAiming = isSemiAiming == false;
            StartCoroutine(AimSwitch());
        }
        if (Input.GetKey(KeyCode.Mouse0) && canShoot == true)
        {
            Shoot();        
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (!isSemiAiming)
            {
                transform.localPosition = Vector3.Slerp(transform.localPosition, aimPos, Time.deltaTime * aimSmooth);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(new Vector3(0, 0, 0)), Time.deltaTime * aimSmooth);
            }
            if (isSemiAiming)
            {
                transform.localPosition = Vector3.Slerp(transform.localPosition, semiAimPos, Time.deltaTime * aimSmooth);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, semiAimRot, Time.deltaTime * aimSmooth) ;
            }
            isAiming = true;
        }
        if (!Input.GetKey(KeyCode.Mouse1) && isReloading == false)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, armPos, Time.deltaTime * aimSmooth);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(new Vector3 (0,0,0)), Time.deltaTime * aimSmooth);
            isAiming = false;
        }
    }
    #region Shooting&Reloading
    private void Shoot()
    {
        if(canShoot == false || ammo <= 0) return;
        ammo -= 1;
        aud.PlayOneShot(shootSound);
        weaponAnimation.SetTrigger("Shoot");
        muzzleFireEffect.Play();
        StartCoroutine(DelayBS());
        recoilScriptCamera.RecoilFire();
        recoilScriptWeapon.RecoilFire();
        RaycastHit hit;
        if(Physics.Raycast(shootPoint.transform.position, shootPoint.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);
            Instantiate(hitEffect.gameObject, hit.point, Quaternion.LookRotation(hit.normal));
        }
        if (ammo == 0) StartCoroutine(Reload());
    }
    IEnumerator Reload()
    {
        isReloading = true;
        weaponAnimation.SetTrigger("Reload");
        ammo = 0;
        canShoot = false;
        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
        isReloading = false;
    }
    IEnumerator DelayBS()
    {
        canShoot = false;
        yield return new WaitForSeconds(delayBetweenShots);
        canShoot = true;
    }
    IEnumerator AimSwitch()
    {
        aimSwitch = false;
        yield return new WaitForSeconds(0.5f);
        aimSwitch = true;
    }
    #endregion
}
