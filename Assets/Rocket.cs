using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    //reference rigidbody in this script
    //Crearte a variable of type of Rigidbody
    Rigidbody rigidBody;

    //Created a variable of type of audiosource
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        //Ask Only to act on the component of ridgidbody(*genetics)
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        // if user input is pressing space, then thrust the rocket
        if (Input.GetKey(KeyCode.Space))
        {
            // type . can explore what we can do to the variable rigidBody
            // Use relative force means it thrust based on the object itself
            // vecter3 means the position x, y, and z 
            rigidBody.AddRelativeForce(Vector3.up);
            // Control not playing the audio eahc time when user press space. 
            // It should only start when the user press apce and when there's no sound is playing
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }    
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
        }
    }
}
