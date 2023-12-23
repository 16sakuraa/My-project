using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    // Start is called before the first frame update
   void OnCollisionEnter(Collision collision)
   {
    
        if(collision.gameObject.name == "Player")
        {
            Debug.Log("enter");
            collision.gameObject.transform.SetParent(transform);
        }
   }

   void OnCollisionExit(Collision collision)
   {
    
        if(collision.gameObject.name == "Player")
        {
            Debug.Log("exit");
            collision.gameObject.transform.SetParent(null);
        }
   }
   
}
