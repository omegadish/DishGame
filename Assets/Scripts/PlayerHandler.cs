using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHandler : MonoBehaviour
{
    public float speed = 5.0f;
    public float thrust = 20.0f;
    float fuel = 20.0f;

    public ParticleSystem thrustParticles;

    public Text verticalSpeedText;
    public Text fuelText;

    public Button restartGameButton;

    bool gameOver = false;

    float fuelTankPickupTimer = 1.0f;

    // Use this for initialization
    void Start () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "FuelTank" && fuelTankPickupTimer < 0.0f)
        {
            Destroy(other.gameObject);
            fuel += 30.0f;
            fuelTankPickupTimer = 1.0f;
        }
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
        fuelTankPickupTimer -= Time.deltaTime;

        Rigidbody rb = GetComponent<Rigidbody>();
        if(!gameOver)
        {
            if (fuel > 0.0f)
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Rotate(Vector3.forward * speed * Time.deltaTime);

                    fuel -= Time.deltaTime * 1.0f;
                }

                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    transform.Rotate(Vector3.back * speed * Time.deltaTime);

                    fuel -= Time.deltaTime * 1.0f;
                }

                if (Input.GetKey(KeyCode.Space))
                {
                    rb.AddForce(transform.up * thrust, ForceMode.Acceleration);
                    thrustParticles.Emit(5);

                    fuel -= Time.deltaTime * 3.0f;
                }
            }

            if(fuel < 20.0f && fuel > 10.0f)
            {
                fuelText.color = Color.yellow;
            }
            else if (fuel <= 10.0f)
            {
                fuelText.color = Color.red;
            }
            else
            {
                fuelText.color = Color.green;
            }
        
            verticalSpeedText.text = "V/S: " + rb.velocity.y.ToString("0.0 m/s");
        }
        else
        {
            verticalSpeedText.text = "GAME OVER!";
            restartGameButton.gameObject.SetActive(true);
        }

        fuelText.text = "FUEL: " + fuel.ToString("0.0 kg");

    }
}
