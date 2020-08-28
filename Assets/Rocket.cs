using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    //reference rigidbody in this script
    //Crearte a variable of type of Rigidbody
    Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        //Ask Only to act on the component of ridgidbody(*genetics)
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        //if user input is pressing space, then thrust the rocket
        if (Input.GetKey(KeyCode.Space))
        {
            //type . can explore what we can do to the variable rigidBody
            //Use relative force means it thrust based on the object itself
            //vecter3 means the position x, y, and z 
            rigidBody.AddRelativeForce(Vector3.up);
        }
        if (Input.GetKey(KeyCode.A))
        {
            print("Rotating left"); 
        }
        else if (Input.GetKey(KeyCode.A))
        {
            print("Rotating Right");
        }
    }
}
