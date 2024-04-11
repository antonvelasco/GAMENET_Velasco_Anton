using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{

    public float bulletSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
   
    void Update()
    {
        transform.Translate(0f, 0f, bulletSpeed * Time.deltaTime);
    }


    [PunRPC]
    public void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") && !col.gameObject.GetComponent<PhotonView>().IsMine)
        {
            col.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, 100);
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
