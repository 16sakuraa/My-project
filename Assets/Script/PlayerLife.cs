using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{   
    [SerializeField] AudioSource deadSound;
    bool dead = false;
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Body"))
        {
            GetComponent<MeshRenderer>().enabled = false;
           GetComponent<Rigidbody>().isKinematic = true;
           GetComponent<PlayerMovement>().enabled = false;
            Die();
            
        }
    }

    private void Update()
    {
        if (transform.position.y < -1f && !dead)
        {
            Debug.Log("yes");
            Die();
        }
    }

    void Die()
    {
        
        dead = true;
        Invoke(nameof(ReloadLevel),1.3f);
        deadSound.Play();
    }
    
    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
