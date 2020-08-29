using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Make thrust that we can modified to become an inspector in the of the object
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    
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
        Thrust();
        Rotate();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Switch based on the collision comes in, collision.ganeObject means you look at the game object you're collliding with
        // and reading its tag and switch based on the string.
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing
                print("OK");//remove this line in future
                break;
            default:
                print("Dead");
                //Kill player
                break;
        }
    }

    private void Thrust()
    {
        // if user input is pressing space, then thrust the rocket
        if (Input.GetKey(KeyCode.Space))
        {
            // type . can explore what we can do to the variable rigidBody
            // Use relative force means it thrust based on the object itself
            // vecter3 means the position x, y, and z 
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            // Control not playing the audio eahc time when user press space. 
            // It should only start when the user press apce and when there's no sound is playing
        }
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        else
        {
            audioSource.Stop();
        }    
    }    
    private void Rotate()
    {
        //control whether physics will change the rotation of the object
        //Take manual control of rotation
        rigidBody.freezeRotation = true;

        
        float rotateThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotateThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotateThisFrame);
        }
        
        rigidBody.freezeRotation = false; // resume physics control of rotation
    }
        
}
// Time.deltaTime tells us the last frame time, so it's good predictor of the current frame time
// rotation = rcsThrust * Time.deltaTime;
// rcsTrust "reaction control thruster thrust vale. 