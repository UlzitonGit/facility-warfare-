using System.Collections;
using TMPro;
using UnityEngine;
using Photon.Pun;
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
    [SerializeField] AudioClip hitSfx;
    [SerializeField] AudioSource aud;
    [SerializeField] private Animator weaponAnimation;
    [SerializeField] LayerMask toIgnore;
    [SerializeField] float aimSmooth = 0.000000000001f;
    [SerializeField] PhotonSoundMananger _photonSoundMananger;
    private int ammo = 0;
   
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
    [SerializeField] GameObject hitMarker;
    float delayWeapon = 0.1f;
    bool delaying = false;
    bool aimSwitch = true;
    int shootingRate = 0;
    float minusVolume = 1;
    float mechanicMixer = 0;
    [SerializeField] float delayedShootRate = 0.017f;
    [SerializeField] TextMeshProUGUI ammoText;
   
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        ammo = maxAmmo;
        //ammoText = GameObject.FindGameObjectWithTag("AmmoText").GetComponent<TextMeshProUGUI>();
        ammoText.text = "ammo: " + ammo.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.R) && isReloading == false)
        {
            StartCoroutine(Reload());
        }
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
    public void PlaySound()
    {
        _photonSoundMananger.PlayShootSFX();
       
    }
   
    #region Shooting&Reloading
    private void Shoot()
    {
        if(canShoot == false || ammo <= 0) return;
        ammo -= 1;
        shootingRate++;
        delaying = true;
        PlaySound();
       
        delayWeapon = 0.1f;
        weaponAnimation.SetTrigger("Shoot");
        
        muzzleFireEffect.Play();
        StartCoroutine(DelayBS());
        recoilScriptCamera.RecoilFire();
        recoilScriptWeapon.RecoilFire();
        ammoText.text = "ammo: " + ammo.ToString();
        RaycastHit hit;
        if(Physics.Raycast(shootPoint.transform.position, shootPoint.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);
            if(hit.transform.GetComponent<Destroyable>() != null )
            {
                hit.transform.GetComponent<Destroyable>().Destory();
            }
            if (hit.transform.GetComponent<PlayerHealth>())
            {
              
                hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, 35);
                
            }
            else PhotonNetwork.Instantiate(hitEffect.name, hit.point, Quaternion.LookRotation(hit.normal));

        }
        if (ammo == 0) StartCoroutine(Reload());
    }
   
    IEnumerator Reload()
    {
        mechanicMixer = 0;
        minusVolume = 1;
        isReloading = true;
        weaponAnimation.SetBool("IsReloading", true);
        weaponAnimation.SetTrigger("Reload");
        ammo = 0;
        canShoot = false;
        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
        canShoot=true;
        weaponAnimation.SetBool("IsReloading", false);
        ammoText.text = "ammo: " + ammo.ToString();
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
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            weaponAnimation.SetBool("Wall", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            weaponAnimation.SetBool("Wall", false);
        }
    }
}
