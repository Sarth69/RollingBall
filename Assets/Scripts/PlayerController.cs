using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float knockbackStrength = 10;
    float verticalInput;
    Rigidbody playerRb;
    private GameObject focalPoint;
    private bool hasPowerup = false;
    public GameObject powerupIndicator;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed);

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.SetActive(true);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(5);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.rigidbody;
            Vector3 awayFromPlayer = collision.transform.position - transform.position;
            enemyRb.AddForce(awayFromPlayer * knockbackStrength, ForceMode.Impulse);
        }
    }
}
