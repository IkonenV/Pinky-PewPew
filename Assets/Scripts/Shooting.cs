using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce;
    public float fireDelay;
    private float delayTimer;
    Vector3 clickPosition;
    public Animator animator;
    bool charging;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        delayTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && delayTimer >= fireDelay && !charging)
        {
            TriggerFire();
            delayTimer = 0;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetBool("Charge", true);
            charging = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            animator.SetBool("Charge", false);
            charging = false;
        }
    }
    public void Fire()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);

        if (plane.Raycast(ray, out float distance))
        {
            clickPosition = ray.GetPoint(distance);
        }
        //Vector3 bulletDirection = new Vector3(clickPosition.x - firePoint.position.x, 0f, clickPosition.z - firePoint.position.z);
        Vector3 bulletDirection = clickPosition - firePoint.position;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bulletDirection.normalized * fireForce, ForceMode.Impulse);
    }
    public void ChargedFire()
    {
        Fire();
        animator.SetBool("Charge", false);
        charging = false;
    }
    public void TriggerFire()
    {
        animator.SetTrigger("Shoot");
    }
}
