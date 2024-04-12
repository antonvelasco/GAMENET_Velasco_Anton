using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class Shooting : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    [Header("HP Components")]
    public float startHealth = 100;
    public float currentHealth;
    public Image healthbar;
    public string bullet;

    [Header("Bullet Spawner")]
    public GameObject bulletSpawnLoc;

    [Header("DeathUI")]
    public TMP_Text deathUI;
    public AudioClip soundShoot;
    private AudioSource audioSource;

    void Start()
    {
        currentHealth = startHealth;
        healthbar.fillAmount = currentHealth / startHealth;
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if(Input.GetMouseButtonDown(0) && currentHealth > 0)
        {
            PhotonNetwork.Instantiate(bullet, bulletSpawnLoc.transform.position, transform.rotation);
            audioSource.PlayOneShot(soundShoot);
        }
    }
    [PunRPC]
    public void TakeDamage(int damage, PhotonMessageInfo info)
    {
        this.currentHealth -= damage;
        this.healthbar.fillAmount = currentHealth / startHealth;
        if (this.currentHealth <= 0)
        {
            gameObject.GetComponent<VehicleMovement>().speed = 0;
            deathUI.text = "DEAD";
        }

    }
}