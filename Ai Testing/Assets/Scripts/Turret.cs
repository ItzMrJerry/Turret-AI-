using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Turret Settings")]
    public float TurretRotationSpeed;
    public float TurretRadius = 4f;
    [Header("Projectile Settings")]
    public float ProjectileSpeed = 5;
    public float fireRate = 0.5F;
    private float nextFire = 0.0F;
    [Header("ObjectLinks")]
    public DetectionSphere detectionSphere;
    public GameObject projectile;
    public Transform ShootPoint;
    public Transform Target;
    private Vector3 defaultPos;
    [Header("Debug")]
    public bool FollowTarget;
    void Start()
    {
        detectionSphere.GetComponent<SphereCollider>().radius = TurretRadius;
        defaultPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
    }

    private void ShootTarget()
    {
        
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Vector3 targetdir = Target.position - transform.position;
            GameObject go = Instantiate(projectile, ShootPoint.position, transform.rotation) as GameObject;
            go.GetComponent<Rigidbody>().AddForce(targetdir * ProjectileSpeed, ForceMode.Impulse);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        TargetInRange();
    }
    private void TargetInRange()
    {
        if (!detectionSphere.inRange) {
            ResetPosition();
        return;
        }
        CheckforObstacle();

    }
    public void CheckforObstacle()
    {
        RaycastHit hit;
        Vector3 targetdir = Target.position - transform.position;
        if (Physics.Raycast(transform.position, targetdir, out hit,Mathf.Infinity))
        {
            
            if(hit.transform.tag == "Target")
            {
                FollowTarget = true;
            }
            else
            {
                FollowTarget = false;
                ResetPosition();
            }
            
        }
        if (FollowTarget)
        {
            ShootTarget();
            LookAtTarget();
            Debug.DrawRay(transform.position, targetdir * hit.distance, Color.yellow);
        }

    }
    private void LookAtTarget()
    {
        Quaternion lookOnLook = Quaternion.LookRotation(Target.position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, TurretRotationSpeed * Time.deltaTime);
    }
    private void ResetPosition()
    {
        Quaternion lookOnLook = Quaternion.LookRotation(new Vector3(defaultPos.x, defaultPos.y, defaultPos.z) - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, (TurretRotationSpeed / 2) * Time.deltaTime);
    }

}
