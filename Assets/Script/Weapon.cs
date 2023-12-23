using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
   public GameObject bulletPrefab;
   public Transform bulletSpawn;
   public float bulletVelocity = 30;
   public float bulletPrefabLifeTime = 3f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        GameObject bullet = Instantiate(bulletPrefab,bulletSpawn.position,Quaternion.identity);
        

        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized * bulletVelocity, ForceMode.Impulse);

        Destroy(bullet,5f);
    }
}
