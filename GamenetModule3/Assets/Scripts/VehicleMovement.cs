using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 50;
    public float rotationSpeed = 200;
    public float currentSpeed = 0;
   

    public bool isControlledEnabled;

    void Start()
    {
        isControlledEnabled = false;
       
    }
    void LateUpdate()
    {
        if (isControlledEnabled)
        {
            float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

            transform.Translate(0, 0, translation);
            currentSpeed = translation;

            transform.Rotate(0, rotation, 0);

           
        }
       
    }

  


}
