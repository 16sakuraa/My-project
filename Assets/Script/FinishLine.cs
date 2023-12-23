using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] AudioSource FinishSound;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            FinishSound.Play();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }
}
