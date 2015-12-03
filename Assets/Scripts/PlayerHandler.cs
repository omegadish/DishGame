using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHandler : MonoBehaviour
{
    public float speed = 5.0f;
    public float thrust = 20.0f;

    public ParticleSystem thrustParticles;

    public Text verticalSpeedText;

    bool gameOver = false;

    // Use this for initialization
    void Start () {
	
	}

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.tag);

        if(other.gameObject.tag == "Landscape")
        {
            if (other.relativeVelocity.magnitude > 1.0)
            {
                gameOver = true;
            }
        }
        else if (other.gameObject.tag == "Platform")
        {
            if (other.relativeVelocity.magnitude > 2.0)
            {
                gameOver = true;
            }
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        Rigidbody rb = GetComponent<Rigidbody>();
        if(!gameOver)
        { 
	        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(Vector3.forward * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
            }

            if(Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(transform.up * thrust, ForceMode.Acceleration);
                thrustParticles.Emit(5);
            }
        
            verticalSpeedText.text = "V/S: " + rb.velocity.y.ToString("0.0 m/s");
        }
        else
        {
            verticalSpeedText.text = "GAME OVER!";
        }

    }
}
