using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    public float speed = 50;
    private float x;
    private float y;
    private float z;
    public bool isControlledEnabled;

    void Start()
    {
        isControlledEnabled = false;
    }
    void LateUpdate()
    {
        if (isControlledEnabled)
        {
           x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
           //y = Input.GetAxis("Jump") * speed * Time.deltaTime;
           z = Input.GetAxis("Vertical") * speed * Time.deltaTime;
           transform.Translate(x,y,z);
        }
    }
}