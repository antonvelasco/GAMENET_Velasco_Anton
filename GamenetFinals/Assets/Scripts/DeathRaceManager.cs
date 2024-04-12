using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Mars : MonoBehaviour
{
    public GameObject[] vehiclePrefabs;
    public Transform[] startingPositions;
    public GameObject[] eliminationTXTUI;
   

    public static Mars instance = null;

    public TMP_Text timeToDestroy;

    public List<GameObject> cars = new List<GameObject>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            object playerSelectionNumber;
         if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(Constants.PLAYER_SELECTION_NUMBER, out playerSelectionNumber))
            {
                Debug.Log((int)playerSelectionNumber);

                int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
                Vector3 instantiatePosition = startingPositions[actorNumber - 1].position;
                PhotonNetwork.Instantiate(vehiclePrefabs[(int)playerSelectionNumber].name, instantiatePosition, Quaternion.identity);
            }
        }

        foreach (GameObject go in eliminationTXTUI)
        {
            go.SetActive(false);
        }

    }
}