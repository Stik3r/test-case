using UnityEngine;

public class LaserController : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Translate(transform.up * Time.fixedDeltaTime * 16, Space.World);

        if (transform.localScale.y < 4)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 0.1f, 0);
        }
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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.name.Contains("_2"))
        {
            Destroy(transform.gameObject);
        }
    }
}
