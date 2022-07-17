using UnityEngine;

public class UFO : MonoBehaviour
{
    private void FixedUpdate()
    {
        Transform ship = GameObject.FindGameObjectWithTag("Ship").transform;
        transform.position = Vector3.MoveTowards(
            transform.position, ship.position, Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.name.Contains("bullet") || collision.transform.name.Contains("laser"))
        {
            EnemyControl.DestroyUFO(transform.gameObject);
            Destroy(transform.gameObject);
        }
    }
}
