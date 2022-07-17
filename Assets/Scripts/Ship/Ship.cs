using UnityEngine;

public class Ship : MonoBehaviour
{
    public GameObject bullet;
    public GameObject laser;
    public GameObject gun;

    float speed = 0;

    static bool speedPressKey;

    private void Start()
    {
        ShipControl.ship = transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.position = ShipControl.Redirect(collision, transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name.Contains("Meteor") || collision.transform.name.Contains("UFO"))
        {
            Destroy(transform.gameObject);
            ShipControl.alive = false;
        }
    }

    private void Rotation()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = ShipControl.RightRotate(transform.rotation);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = ShipControl.LeftRotate(transform.rotation);
        }
    }

    private void Fly()
    {
        speedPressKey = false;
        if (Input.GetKey(KeyCode.W))
        {
            ShipControl.lastDirectional = transform.up;
            speed = speed >= 4 ? 4 : speed + ShipControl.boost;
            transform.Translate(transform.up * Time.fixedDeltaTime * speed, Space.World);
            ShipControl.speed = speed;
            speedPressKey = true;
        }
    }

    private void Shoot()
    {
        ShipControl.delay++;
        if (Input.GetMouseButton(0) && ShipControl.delay >= 9)
        {
            Instantiate(bullet, gun.transform.position, transform.rotation);
            ShipControl.delay = 0;
        }
        if (Input.GetMouseButton(1) && ShipControl.laserDelay <= Time.time && ShipControl.laserCount != 0)
        {
            Instantiate(laser, gun.transform.position, transform.rotation);
            ShipControl.laserDelay = Time.time + ShipControl.laselCooldown;
            ShipControl.laserCount--;
        }
    }

    private void FixedUpdate()
    {
        Rotation();
        Fly();
        Shoot();

        if (!speedPressKey)
        {
            speed = speed <= 0 ? 0 : speed - ShipControl.brakingSpeed;
            transform.Translate(ShipControl.lastDirectional * Time.fixedDeltaTime * speed, Space.World);
        }

        transform.rotation = ShipControl.ResidualMovementLeft(transform.rotation);
        transform.rotation = ShipControl.ResidualMovementRight(transform.rotation);

        ShipControl.speed = speed;
    }
}
