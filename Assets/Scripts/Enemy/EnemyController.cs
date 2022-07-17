using System.Collections.Generic;
using UnityEngine;

public static class EnemyControl
{
    
    public static List<GameObject> meteors = new List<GameObject>(); // метеоры на карте
    public static List<GameObject> ufos = new List<GameObject>();   //тарекли на карте
    public static int[] meteorsSpeed = { 1, 2, 3};

    public static GameObject[] spawnPoints;
    public static GameObject[] meteorsPrefab;
    public static GameObject ufo;

    public static int bigMeteors = 0;
    public static int maxBigMeteors = 4;

    public static void CreateNewUFO(GameObject ufo)
    {
        ufos.Add(ufo);
    }
    public static void CreateNewMeteor(ref GameObject meteor, bool isBig = false, int meteorLvL = 0)
    {
        Vector3 buf = Random.rotation.eulerAngles;
        buf.x = 0;
        buf.y = 0;
        meteor.transform.rotation = Quaternion.Euler(buf);
        meteor.GetComponent<Meteor>().speed = meteorsSpeed[Random.Range(0, meteorsSpeed.Length)];
        if (isBig)
        {
            meteors.Add(meteor);
            return;
        }
        if(meteorLvL == 0)
        {
            meteor.transform.localScale = new Vector3(0.2f, 0.2f, 0);
            meteor.GetComponent<Meteor>().meteorLvL = 1;
        }
        else
        {
            meteor.transform.localScale = new Vector3(0.13f, 0.13f, 0);
            meteor.GetComponent<Meteor>().meteorLvL = 2;
        }
        meteors.Add(meteor);
        return;
    }

    public static void DestroyMeteor(GameObject meteor)
    {
        if(meteor.GetComponent<Meteor>().meteorLvL == 0)
        {
            bigMeteors--;
        }
        meteors.Remove(meteor);
    }

    public static void DestroyUFO(GameObject ufo)
    {
        ufos.Remove(ufo);
    }

    public static void DestroyAll()
    {
        foreach(var meteor in meteors)
        {
            GameObject.Destroy(meteor);
        }
        meteors.Clear();
        foreach (var ufo in ufos)
        {
            GameObject.Destroy(ufo);
        }
        meteors.Clear();
        ufos.Clear();
    }

    static public Vector3 Redirect(Collider2D collision, Vector3 position)
    {
        if (collision.name == "TopBotton")
        {
            float newPosY = Mathf.Abs(position.y);
            float sign = newPosY / position.y;
            newPosY -= 0.1f;
            return new Vector3(position.x, -(newPosY * sign), 0);
        }
        else
        {
            float newPosX = Mathf.Abs(position.x);
            float sign = newPosX / position.x;
            newPosX -= 0.1f;
            return new Vector3(-(newPosX * sign), position.y, 0);
        }
    }

    public static GameObject SpawnMeteor(GameObject parent = null)
    {
        int indxMeteor = Random.Range(0, meteorsPrefab.Length);
        if (parent == null)
        {
            int indxSpawn = Random.Range(0, spawnPoints.Length);
            return GameObject.Instantiate(
                meteorsPrefab[indxMeteor], spawnPoints[indxSpawn].transform.position, Quaternion.identity);
        }
        return GameObject.Instantiate(
                meteorsPrefab[indxMeteor], parent.transform.position, Quaternion.identity);
    }

    public static GameObject SpawnUfos()
    {
        int indxSpawn = Random.Range(0, spawnPoints.Length);
        return GameObject.Instantiate(ufo, spawnPoints[indxSpawn].transform.position, Quaternion.identity);
    }
}
