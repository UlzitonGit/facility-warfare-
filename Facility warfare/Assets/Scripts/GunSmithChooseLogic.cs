using TMPro;
using UnityEngine;

public class GunSmithChooseLogic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject[] weapons;
    [SerializeField] TextMeshProUGUI equipText;
    int weaponIndex = 0;
    public int currentWeapon = 0;
   
    void Start()
    {
        weaponIndex = PlayerPrefs.GetInt("weapon");
        for (int i = 0; i < weapons.Length; i++)
        {
            if(weaponIndex == i)
            {
                weapons[i].SetActive(true);
                currentWeapon = i;
            }
        }
        WeaponInfo();
    }
    private void WeaponInfo()
    {
        if(currentWeapon == weaponIndex)
        {
            equipText.text = "Equipped";
        }
        else equipText.text = "Equip";
    }
    public void LeftButtonClicked()
    {
        weapons[currentWeapon].SetActive(false);
        if (currentWeapon <= 0)
        {
            weapons[weapons.Length - 1].SetActive(true);
            currentWeapon = weapons.Length - 1;
        }
        else
        {
            weapons[currentWeapon - 1].SetActive(true);
            currentWeapon -= 1;
        }
       

        
        WeaponInfo();
    }
    public void RightButtonClicked()
    {
        weapons[currentWeapon].SetActive(false);
        if (currentWeapon >= weapons.Length - 1)
        {
            weapons[0].SetActive(true);
            currentWeapon = 0;
        }
        else
        {
            weapons[currentWeapon + 1].SetActive(true);
            currentWeapon ++;
        }
       
        
        WeaponInfo();
    }
    public void Equip()
    {
        weaponIndex = currentWeapon;
        PlayerPrefs.SetInt("weapon", weaponIndex);
        WeaponInfo();
    }
}
