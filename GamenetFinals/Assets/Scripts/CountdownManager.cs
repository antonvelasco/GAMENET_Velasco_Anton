using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class CountdownManager : MonoBehaviourPunCallbacks
{
  
    public TMP_Text timerText;

    public float timeToStartRace = 5.0f;

    public float timeToHunt = 3.0f;
    // Start is called before the first frame update
    void Start()
    {

        timerText = Mars.instance.timeToDestroy;
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (timeToStartRace > 0)
            {

                timeToStartRace -= Time.deltaTime;
                photonView.RPC("SetTime", RpcTarget.AllBuffered, timeToStartRace);
            }
            else if (timeToStartRace < 0)
            {
                photonView.RPC("StartRace", RpcTarget.AllBuffered);
                
            }
        }
    }

    [PunRPC]
    public void SetTime(float time)
    {
        if(time > 0)
        {
            timerText.GetComponent<TextMeshProUGUI>().text = time.ToString("F1");

        }
        else
        {
            timerText.GetComponent<TextMeshProUGUI>().text = " ";
        }
    }

    [PunRPC]
    public void StartRace()
    {
        GetComponent<VehicleMovement>().isControlledEnabled = true;
        this.enabled = false;
    }

}
