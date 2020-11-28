using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject[] spawnPoints;
    [SerializeField]
    GameObject[] powerUpSpawns;
    [SerializeField]
    GameObject tanker;
    [SerializeField]
    GameObject ranger;
    [SerializeField]
    GameObject soldier;
    [SerializeField]
    GameObject arrow;
    [SerializeField]
    GameObject healthPowerUp;
    [SerializeField]
    GameObject speedPowerUp;
    [SerializeField]
    Text levelText;
    [SerializeField]
    int maxPowerUps = 4;

    private bool gameOver = false;
    private int currentLevel;
    private float generatedSpawnTime = 1;
    private float currentSpawnTime = 0;
    private float powerUpSpawnTime = 30;
    private float currentPowerUpSpawnTime = 0;
    private GameObject newEnemy;
    private int powerups = 0;
    private GameObject newPowerUp;

    private List<EnemyHealth> enemies = new List<EnemyHealth>();
    private List<EnemyHealth> killedEnemies = new List<EnemyHealth>();

    public void RegisterEnemy(EnemyHealth enemy)
    {
        enemies.Add(enemy);
    }

    public void KilledEnemy(EnemyHealth enemy)
    {
        killedEnemies.Add(enemy);
    }

    public void RegisterPowerUp ()
    {
        powerups++;
    }

    public bool GameOver
    {
        get
        {
            return gameOver;
        }
    }

    public GameObject Player
    {
        get
        {
            return player;
        }
    }

    public GameObject Arrow
    {
        get
        {
            return arrow;
        }
    }

    static public GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawn());
        currentLevel = 1;
        StartCoroutine(powerUpSpawn());

    }

    // Update is called once per frame
    void Update()
    {
        currentSpawnTime += Time.deltaTime;
        currentPowerUpSpawnTime += Time.deltaTime;
    }

    public void PlayerHit(int currentHP)
    {
        if (currentHP > 0)
            gameOver = false;
        else
            gameOver = true;
    }

    //how many enemies when and where to spawn
    
    IEnumerator spawn()
    {
        if (currentSpawnTime > generatedSpawnTime)
        {
            currentSpawnTime = 0;

            if (enemies.Count < currentLevel)
            {
                //spawn random enemy at a random spawn point
                int randomNumber = Random.Range(0, spawnPoints.Length);
                GameObject spawnLocation = spawnPoints[randomNumber];
                int randomEnemy = Random.Range(0, 3);
                if (randomEnemy == 0)
                {
                    newEnemy = Instantiate(soldier);
                }
                else if (randomEnemy == 1)
                {
                    newEnemy = Instantiate(ranger);
                }
                else if (randomEnemy == 2)
                {
                    newEnemy = Instantiate(tanker);
                }
                //enemy spawns at the location of the spawn point
                newEnemy.transform.position = spawnLocation.transform.position;
            }

            if (killedEnemies.Count == currentLevel)
            {
                enemies.Clear();
                killedEnemies.Clear();

                yield return new WaitForSeconds(3f);
                currentLevel++;
                levelText.text = "Level " + currentLevel;
            }
        }

        yield return null;
        StartCoroutine(spawn());
    }

    IEnumerator powerUpSpawn()
    {
        if (currentPowerUpSpawnTime > powerUpSpawnTime)
        {
            currentPowerUpSpawnTime = 0;

            if (powerups < maxPowerUps)
            {
                int randomNumber = Random.Range(0, powerUpSpawns.Length);
                GameObject spawnLocation = powerUpSpawns[randomNumber];
                //min is inclusive and max is exclusive for the random.range
                int randomPowerUp = Random.Range(0, 2);
                if (randomPowerUp == 0)
                {
                    newPowerUp = Instantiate(healthPowerUp);
                }
                else if (randomPowerUp == 1)
                {
                    newPowerUp = Instantiate(speedPowerUp);
                }
                newPowerUp.transform.position = spawnLocation.transform.position;
            }

        }

        yield return null;
        StartCoroutine(powerUpSpawn());
    }
}
