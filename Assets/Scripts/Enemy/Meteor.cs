using UnityEngine;

public class Meteor : MonoBehaviour
{
    public GameObject[] meteors;

    public int speed = 0;
    public int meteorLvL = 0;


    private void FixedUpdate()
    {
        transform.Translate(transform.up * Time.fixedDeltaTime * speed, Space.World);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.position = EnemyControl.Redirect(collision, transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name.Contains("bullet") || collision.transform.name.Contains("laser"))
        {
            EnemyControl.DestroyMeteor(transform.gameObject);
            if(meteorLvL != 2)
            {
                for(int i = 0; i < 2; i++)
                {
                    GameObject meteor = EnemyControl.SpawnMeteor(transform.gameObject);
                    EnemyControl.CreateNewMeteor(ref meteor, false, meteorLvL);
                }
            }
            Destroy(transform.gameObject);
        }
    }
}
