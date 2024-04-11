using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class Shooting : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    [Header("HP Components")]
    public float startHealth = 100;
    public float currentHealth;
    public Image healthbar;

    [Header("Hit Effects")]
    public GameObject hiteffectPrefab;

    [Header("Bullet Spawner")]
        public GameObject bulletSpawnLoc;

    void Start()
    {
        currentHealth = startHealth;
        healthbar.fillAmount = currentHealth / startHealth;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }
        else if(Input.GetMouseButtonDown(1))
        {
            PhotonNetwork.Instantiate("Bullet", bulletSpawnLoc.transform.position, transform.rotation);
        }
    }

    public void Fire()
    {
        RaycastHit hit;
        Ray ray = GetComponent<PlayerSetup>().camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        if(Physics.Raycast(ray, out hit, 200))
        {
            Debug.Log(hit.collider.gameObject.name);
           // photonView.RPC("CreateHiteffects", RpcTarget.All, hit.point);

            if (hit.collider.gameObject.CompareTag("Player") && !hit.collider.gameObject.GetComponent<PhotonView>().IsMine)
            {
                hit.collider.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, 25);
            }


        }
    }

    [PunRPC]
    public void TakeDamage(int damage, PhotonMessageInfo info)
    {
        this.currentHealth -= damage;

        this.healthbar.fillAmount = currentHealth / startHealth;

        Debug.Log(info.Sender.NickName + " Binaril si  " + info.photonView.Owner.NickName);
        Debug.Log(info.photonView.Owner.NickName + "HP: " + currentHealth);

       }

    [PunRPC]
    public void CreateHiteffects(Vector3 position)
    {
        GameObject hitEffectGameObject = Instantiate(hiteffectPrefab, position, Quaternion.identity);
        Destroy(hitEffectGameObject, 0.25f);
    }


}
