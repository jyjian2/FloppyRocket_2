using UnityEngine;
using UnityEngine.SceneManagement;
//scene management to control switching in different level

public class Rocket : MonoBehaviour
{
    // Make thrust that we can modified to become an inspector in the of the object
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;
    
    //reference rigidbody in this script
    //Crearte a variable of type of Rigidbody
    Rigidbody rigidBody;
    //Created a variable of type of audiosource
    AudioSource audioSource;
    // Start is called before the first frame update

    // List three state 
    enum State { Alive, Dying, Transcending }
    State state = State.Alive;
    void Start()
    {
        //Ask Only to act on the component of ridgidbody(*genetics)
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // There's one second waiting time for loading another level.
        //Add this condition to allow user to only play somthing when the status is Alive
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive){ return; } // ignore collision when dead
        // Switch based on the collision comes in, collision.ganeObject means you look at the game object you're collliding with
        // and reading its tag and switch based on the string.
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing
                break;

            case "Finish":
                StartSuccessSequence();
                break;

            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        Invoke("LoadNextLevel", 1f);
        // called routine. Points down to the method "LoadNextLevel" after one second
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        Invoke("LoadFirstLevel", 1f); // parameterise time
    }


    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1); // allow for more than 2 levels

    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void RespondToThrustInput()
    {
        // if user input is pressing space, then thrust the rocket
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
        }    
    }

    private void ApplyThrust()
    {
        // type . can explore what we can do to the variable rigidBody
        // Use relative force means it thrust based on the object itself
        // vecter3 means the position x, y, and z 
        // Control not playing the audio eahc time when user press space. 
        // It should only start when the user press apce and when there's no sound is playing
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);
        if (!audioSource.isPlaying) // so it doesn't layer
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }

    private void RespondToRotateInput()
    {
        //control whether physics will change the rotation of the object
        //Take manual control of rotation
        rigidBody.freezeRotation = true; //take mainual control of rotation

        // if user input is pressing space, then thrust the rocket

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