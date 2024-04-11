using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.UI;
using TMPro;

public class LapController : MonoBehaviourPunCallbacks
{
    public List<GameObject> lapTriggers = new List<GameObject>();

    public enum RaiseEventsCode
    {
        WhofinishedEventCode = 0,
      
    }

    public int finsihOrder = 0;

    private void OnEnable()
    {
      
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    private void OnDisable()
    {
        
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }
    void OnEvent(EventData photonEvent)
    {
        if(photonEvent.Code == (byte)RaiseEventsCode.WhofinishedEventCode)
        {
            object[] data = (object[])photonEvent.CustomData;

            string nickNameOfFinishedPlayer = (string)data[0];
            finsihOrder = (int)data[1];
            int viewId = (int)data[2];

            Debug.Log(nickNameOfFinishedPlayer + " " + finsihOrder);
            GameObject orderUIText = RacingGameManager.instance.finisherTextUI[finsihOrder - 1];
            orderUIText.SetActive(true);

            if(viewId == photonView.ViewID) // this is you
            {
                orderUIText.GetComponent<TextMeshProUGUI>().text = finsihOrder + " YOU " + nickNameOfFinishedPlayer;
                orderUIText.GetComponent<TextMeshProUGUI>().color = Color.red;
            }
            else
            {
                orderUIText.GetComponent<TextMeshProUGUI>().text = finsihOrder + " " + nickNameOfFinishedPlayer;
            }
            orderUIText.GetComponent<TextMeshProUGUI>().text = finsihOrder + " " + nickNameOfFinishedPlayer;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject go in RacingGameManager.instance.lapTriggers)
        {
            lapTriggers.Add(go);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(lapTriggers.Contains(col.gameObject))
        {
            int indexOfTrigger = lapTriggers.IndexOf(col.gameObject);

            lapTriggers[indexOfTrigger].SetActive(false);
        }

        if(col.gameObject.tag == "FinishTrigger")
        {
            Debug.Log("Finish Trigger");
            GameFinish();
        }
    }

    public void GameFinish()
    {
        GetComponent<VehicleMovement>().enabled = false;
        GetComponent<PlayerSetup>().camera.transform.parent = null;

        finsihOrder++;
        string nickName = photonView.Owner.NickName;
        int viewId = photonView.ViewID;
        //event data

        object[] data = new object[] { nickName, finsihOrder, viewId };

        RaiseEventOptions raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All,
            CachingOption = EventCaching.AddToRoomCache
    };


        SendOptions sendOption = new SendOptions
        {
            Reliability = false
        };
       PhotonNetwork.RaiseEvent((byte)RaiseEventsCode.WhofinishedEventCode, data, raiseEventOptions, sendOption);
    }


}
