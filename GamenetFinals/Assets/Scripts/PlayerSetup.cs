using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    public Camera _camera;

    [SerializeField] TextMeshProUGUI nameOfPlayer;
    // Start is called before the first frame update
    void Start()
    {
        this._camera = transform.Find("Camera").GetComponent<Camera>();
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue("dr"))
        {
            GetComponent<VehicleMovement>().enabled = photonView.IsMine;
            GetComponent<Shooting>().enabled = photonView.IsMine;
           _camera.enabled = photonView.IsMine;
        }
        nameOfPlayer.text = photonView.Owner.NickName;
    }
}
