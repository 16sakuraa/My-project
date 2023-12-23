using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MissileLauncher : MonoBehaviour
{

    [SerializeField] Text lockOnCount;
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private Transform Subject = null;
    [SerializeField] private Text targetText;
    [SerializeField] private Image targetMark;
    public float lockOnAngle = 50f;
    public float maxLockOnAngle = 65f; // Maximum angle to maintain lock-on
    public Transform missilePrefab;
    public int maxLocks = 3;
    public float missileSpeed = 10f;

    public float yourNewLockOnDistance = 50f;

    public Transform rocketSpawn;

    private int currentLocks = 0;
    private Transform[] lockedTargets = new Transform[3];
    private Camera mainCamera;

    public float missileFireDelay = 0.3f; // Set the delay between missile fires
    public float lockOnDelay = 1.0f;

    private bool isFiring = false;
    
    [SerializeField] AudioSource fireSound;
    [SerializeField] AudioSource lockSound;


    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            TryLockOnTarget();
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            if (!isFiring)
            {
                StartCoroutine(FireMissilesWithDelay());
            }
        }

        if (Subject != null)
        {

            if (targetMark != null)
            {

                targetMark.enabled = true;
        
                Vector3 screenPosition = PlayerCamera.WorldToScreenPoint(Subject.position);
                targetMark.transform.position = screenPosition;
                
            }
        }
    }
    

    IEnumerator FireMissilesWithDelay()
{
    isFiring = true;

    for (int i = 0; i < currentLocks; i++)
    {
        Transform missile = Instantiate(missilePrefab, rocketSpawn.position, Quaternion.identity);
        missile.GetComponent<HomingMissile>().SetTarget(lockedTargets[i], missileSpeed);
        fireSound.Play();

        yield return new WaitForSeconds(missileFireDelay); // Wait for the specified delay
    }

    // Reset locks after firing
    currentLocks = 0;
    lockOnCount.text = "Missile Locks On: " + currentLocks;
    targetText.enabled = false;
    targetMark.enabled = false;
    Subject = null;
    System.Array.Clear(lockedTargets, 0, lockedTargets.Length);

    isFiring = false;
}

    void TryLockOnTarget()
{
    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
    ray.origin = mainCamera.transform.position; // Ensure the ray starts from the camera position
    ray.direction = mainCamera.transform.forward; // Use the camera's forward direction
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit, yourNewLockOnDistance) && hit.collider.CompareTag("Target"))
    {
        Vector3 targetDirection = hit.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, targetDirection);

        if (angle < maxLockOnAngle)
        {
            if (!IsTargetLocked(hit.transform))
            {

                LockOnTarget(hit.transform);
                Debug.Log("Lock Acquired");
            }
        }
        else if (currentLocks > 0)
        {
            currentLocks = 0;
            System.Array.Clear(lockedTargets, 0, lockedTargets.Length);
        }
    }
    StartCoroutine(LockOnDelay());
    
}

IEnumerator LockOnDelay()
{
    yield return new WaitForSeconds(lockOnDelay);
}

    void LockOnTarget(Transform target)
    {
        if (currentLocks < maxLocks)
        {
            lockedTargets[currentLocks] = target;
            currentLocks++;
            Debug.Log("Locks on ; " + currentLocks);
            lockOnCount.text = "Missile Locks On : " + currentLocks;

            Subject = target;
            lockSound.Play();
            
            
            
        

        }
    }

    bool IsTargetLocked(Transform target)
    {
        return System.Array.IndexOf(lockedTargets, target) != -1;
        // return false;
    }
}
