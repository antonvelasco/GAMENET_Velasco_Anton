using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

public class Elimination : MonoBehaviourPunCallbacks
{
    public List<GameObject>  cars = new List<GameObject>();

    public Shooting hpOfPlayer;
    public enum RaiseEventsCode
    {
        EliminationOrder = 0
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

    void Start()
    {
        foreach (GameObject go in Mars.instance.cars)
        {
           cars.Add(go);
        }
    }
}