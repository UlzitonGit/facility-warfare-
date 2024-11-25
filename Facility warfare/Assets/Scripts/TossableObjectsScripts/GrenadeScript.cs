using Photon.Pun;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{

    public float radius = 300f;
    public float damage = 50f;
    private void OnCollisionEnter(Collision collision)
    {
        PhotonView check = collision.gameObject.GetComponent<PhotonView>();
        if (check != null)
        {
            if(check.IsMine == false)
            {
                DamageDeal();
            }
        }
        else
        {
            DamageDeal();
        }
    }
    private void DamageDeal()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            PhotonView player = nearbyObject.GetComponent<PhotonView>();
            if (player != null)
            {
                player.RPC("TakeDamage", RpcTarget.All, damage);
            }
        }
        Debug.Log("1");
        Destroy(gameObject);
    }
}
