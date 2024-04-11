using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    public Camera camera;

    [SerializeField] TextMeshProUGUI nameOfPlayer;
    // Start is called before the first frame update
    void Start()
    {
        this.camera = transform.Find("Camera").GetComponent<Camera>();
        if(PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue("rc"))
        {
            GetComponent<VehicleMovement>().enabled = photonView.IsMine;
            GetComponent<LapController>().enabled = photonView.IsMine;
            camera.enabled = photonView.IsMine;
        }
        else if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue("dr"))
        {
            GetComponent<VehicleMovement>().enabled = photonView.IsMine;
            GetComponent<Shooting>().enabled = photonView.IsMine;
            // GetComponent<LapController>().enabled = photonView.IsMine;
         //   GetComponent<Elimination>().enabled = photonView.IsMine;
            camera.enabled = photonView.IsMine;

            
        }

        nameOfPlayer.text = photonView.Owner.NickName;
    }

   
   
}
