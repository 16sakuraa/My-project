using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    private Transform target;
    private float speed = 10f;
    
    [SerializeField] AudioSource impactSound;
    [SerializeField] AudioSource FlightSound;

    void Start()
    {
        FlightSound.Play();
    }

    public void SetTarget(Transform newTarget, float missileSpeed)
    {
        target = newTarget;
        speed = missileSpeed;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Calculate the direction to the target
        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        // Rotate towards the target
        transform.rotation = Quaternion.LookRotation(direction);

        // Move forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            impactSound.Play();
            print("hit " + collision.gameObject.name + " with a rocket");
            Destroy(gameObject);
                      
        }
        else
        {
            impactSound.Play();
            print("didnt hit the target");
            Destroy(gameObject);
        }
    }
}