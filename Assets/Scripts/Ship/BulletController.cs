using UnityEngine;

public class BulletController : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * Time.fixedDeltaTime * 9);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.Destroy(transform.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name.Contains("Meteor"))
        {
            ShipControl.score += 20;
            
        }
        if (collision.transform.name.Contains("UFO"))
        {
            ShipControl.score += 10 * 5;
        }
        GameObject.Destroy(transform.gameObject);
    }
}
