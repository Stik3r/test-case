using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public GameObject ship; 
    public GameObject ufo;
    public GameObject[] meteors;
    public GameObject[] spawnPoints;    //префабы и точки респавна

    public GameObject score;
    public GameObject speed;
    public GameObject X;
    public GameObject Y;
    public GameObject degrees;
    public GameObject laserCount;
    public GameObject laserCD;
    public GameObject gameOverPanel;    //UI элементы
    public GameObject resultScore;

    private int maxUfo = 2;

    private float timer;
    private float delay = 10f;
    private float laserTimer;
    private float laserAddAmmo = 25f;   //задержки респавна и время для нового лазера


    private void Start()
    {
        EnemyControl.meteorsPrefab = meteors;
        EnemyControl.spawnPoints = spawnPoints;
        EnemyControl.ufo = ufo;

        Instantiate(ship);
        SpawnEnemies(true);
        timer = Time.time + delay;
        laserTimer = Time.time + laserAddAmmo;
    }

    private void Update()
    {
        if (ShipControl.alive)
        {
            if (timer <= Time.time)
            {

                SpawnEnemies();
                timer = Time.time + delay;
            }
            if (laserTimer <= Time.time)
            {
                if (ShipControl.laserCount < 3)
                {
                    ShipControl.laserCount++;
                }
                laserTimer = Time.time + laserAddAmmo;
            }
            ViewInfo();
        }
        else
            EndGame();
    }

    
    /// <summary>
    /// Запуск новой игры после смерти
    /// </summary>
    public void NewGame()
    {
        ShipControl.Reset();
        EnemyControl.DestroyAll();
        foreach(var bullet in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Destroy(bullet);
        }
        ShipControl.ship = Instantiate(ship).transform;
        SpawnEnemies(true);
        gameOverPanel.SetActive(false);
        ShipControl.alive = true;
    }

    /// <summary>
    /// Создание противников
    /// </summary>
    private void SpawnEnemies(bool firstEnemy = false)
    {
        if (firstEnemy)
        {
            GameObject m = EnemyControl.SpawnMeteor();
            EnemyControl.CreateNewMeteor(ref m, true);
            EnemyControl.bigMeteors++;
            return;
        }

        if(EnemyControl.bigMeteors < EnemyControl.maxBigMeteors && EnemyControl.ufos.Count < maxUfo)
        {
            int flip = Random.Range(0, 2);
            if (flip == 0)
            {
                GameObject m = EnemyControl.SpawnMeteor(); 
                EnemyControl.CreateNewMeteor(ref m, true);
                EnemyControl.bigMeteors++;
            } 
            else
                EnemyControl.CreateNewUFO(EnemyControl.SpawnUfos());
            return;
        }

        if(EnemyControl.bigMeteors < EnemyControl.maxBigMeteors)
        {
            GameObject m = EnemyControl.SpawnMeteor();
            EnemyControl.CreateNewMeteor(ref m, true);
            EnemyControl.bigMeteors++;
        }
        else
            EnemyControl.CreateNewUFO(EnemyControl.SpawnUfos());
    }

    /// <summary>
    /// Вывод информации на экран(лазер, позиция и тд.)
    /// </summary>
    private void ViewInfo()
    {
        Transform ship = ShipControl.ship;
        score.GetComponent<Text>().text = ShipControl.score.ToString();
        X.GetComponent<Text>().text = System.Math.Round(ship.position.x, 1).ToString();
        Y.GetComponent<Text>().text = System.Math.Round(ship.position.y, 1).ToString();
        degrees.GetComponent<Text>().text = System.Math.Round(ship.rotation.eulerAngles.z, 1).ToString() + "°";
        speed.GetComponent<Text>().text = (System.Math.Round(ShipControl.speed * 10, 1)).ToString() + "м/с";
        laserCount.GetComponent<Text>().text = ShipControl.laserCount.ToString();
        float buf = ShipControl.laserDelay - Time.time <= 0 ? 0 : ShipControl.laserDelay - Time.time;
        laserCD.GetComponent<Text>().text = System.Math.Round(buf, 1).ToString();
    }


    public void EndGame()
    {
        gameOverPanel.SetActive(true);
        resultScore.GetComponent<Text>().text = ShipControl.score.ToString();
        foreach(var ufo in EnemyControl.ufos)
        {
            Destroy(ufo.GetComponent<UFO>());
        }
    }

}
