using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Tossables : MonoBehaviour
{
    [Header("Rederences")]
    public Transform camera;
    public Transform attackPoint;
    public GameObject objectToThrow;

    [Header("Setting")]
    public int totalThrows; // количество бросаемых объектов
    public float throwCooldown; // кд

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.G;
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    private void Start()
    {
        readyToThrow = true;
    }
    void Update()
    {
        if (Input.GetKeyUp(throwKey) && readyToThrow && totalThrows > 0)
        {
            Throw();
        }
    }

    private void Throw()
    {
        readyToThrow = false;
        // Создание объекта
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, camera.rotation);

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // Направление
        Vector3 forceDirection = camera.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(camera.position, camera.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // Бросание
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        StartCoroutine(Reload());
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(throwCooldown);
        readyToThrow = true;
    }
}