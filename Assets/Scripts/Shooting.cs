using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce;
    public float fireDelay;
    private float delayTimer;
    Vector3 clickPosition;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        delayTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && delayTimer >= fireDelay)
        {
            Fire();
            delayTimer = 0;
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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bulletDirection.normalized * fireForce, ForceMode.Impulse);
    }
}
