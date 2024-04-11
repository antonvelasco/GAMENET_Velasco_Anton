using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.UI;
using TMPro;

public class Elimination : MonoBehaviourPunCallbacks
{
    public List<GameObject>  cars = new List<GameObject>();

    public Shooting hpOfPlayer;
    public enum RaiseEventsCode
    {
        EliminationOrder = 0
    }

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
        if (photonEvent.Code == (byte)RaiseEventsCode.EliminationOrder)
        {
            object[] data = (object[])photonEvent.CustomData;

            int eliminatedPlayerID = (int)data[0];
            string nickNameOfEliminatedPlayer = (string)data[0];

            if (hpOfPlayer.currentHealth <= 0)
            {
                Debug.Log(nickNameOfEliminatedPlayer + " has been Eliminated");
            }

        }

      
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject go in DeathRaceManager.instance.cars)
        {
           cars.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
