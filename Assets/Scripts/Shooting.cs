using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform firePoint2;
    public Transform firePoint3;
    public Transform firePoint4;
    public Transform firePoint5;
    public float fireForce;
    public float fireDelay;
    private float delayTimer;
    Vector3 clickPosition;
    public Animator animator;
    bool charging;
    public ParticleSystem chargeParticles;
    public ParticleSystem chargeBloom;
    

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
            chargeParticles.Play();
            chargeBloom.Play();
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            animator.SetBool("Charge", false);
            charging = false;
            chargeParticles.Stop();
            chargeBloom.Stop();
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
        Vector3 bulletDirection = new Vector3(clickPosition.x - firePoint.position.x, 0f, clickPosition.z - firePoint.position.z);
        //Vector3 bulletDirection = clickPosition - firePoint.position;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(firePoint.forward.normalized * fireForce, ForceMode.Impulse);
    }
    public void Fire2()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);

        if (plane.Raycast(ray, out float distance))
        {
            clickPosition = ray.GetPoint(distance);
        }
        Vector3 bulletDirection = new Vector3(clickPosition.x - firePoint.position.x, 0f, clickPosition.z - firePoint.position.z);
        //Vector3 bulletDirection = clickPosition - firePoint.position;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(firePoint2.forward.normalized * fireForce, ForceMode.Impulse);
    }
    public void Fire3()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);

        if (plane.Raycast(ray, out float distance))
        {
            clickPosition = ray.GetPoint(distance);
        }
        Vector3 bulletDirection = new Vector3(clickPosition.x - firePoint.position.x, 0f, clickPosition.z - firePoint.position.z);
        //Vector3 bulletDirection = clickPosition - firePoint.position;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(firePoint3.forward.normalized * fireForce, ForceMode.Impulse);
    }
        public void Fire4()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);

        if (plane.Raycast(ray, out float distance))
        {
            clickPosition = ray.GetPoint(distance);
        }
        Vector3 bulletDirection = new Vector3(clickPosition.x - firePoint.position.x, 0f, clickPosition.z - firePoint.position.z);
        //Vector3 bulletDirection = clickPosition - firePoint.position;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(firePoint4.forward.normalized * fireForce, ForceMode.Impulse);
    }
    public void Fire5()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);

        if (plane.Raycast(ray, out float distance))
        {
            clickPosition = ray.GetPoint(distance);
        }
        Vector3 bulletDirection = new Vector3(clickPosition.x - firePoint.position.x, 0f, clickPosition.z - firePoint.position.z);
        //Vector3 bulletDirection = clickPosition - firePoint.position;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(firePoint5.forward.normalized * fireForce, ForceMode.Impulse);
    }
    public void ChargedFire()
    {
        Fire();
        Fire2();
        Fire3();
        Fire4();
        Fire5();
        animator.SetBool("Charge", false);
        charging = false;
    }
    public void TriggerFire()
    {
        animator.SetTrigger("Shoot");
    }
}
