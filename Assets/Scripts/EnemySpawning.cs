using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [Header("Spawn Enemy:")]
    [SerializeField] private GameObject Enemy;
    [SerializeField] private float SpawnDelay;
    [SerializeField] private float SpawnPos;
    [SerializeField] public float SpawnLimit;
    [SerializeField] public float SpawnCount;

    private GameObject Player;
    private float MaxSpawnDelay;
    private GameObject[] _Enemy;

    // Start is called before the first frame update
    void Start()
    {
        MaxSpawnDelay = SpawnDelay;

        SpawnCount = 0;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        _Enemy = GameObject.FindGameObjectsWithTag("Enemy");

        if (Player && SpawnDelay <= 0.0f && SpawnCount < SpawnLimit)
        {
            // Get a Random x and y value
            float Random_x = Random.Range(-SpawnPos, SpawnPos);
            float Random_y = Random.Range(-SpawnPos, SpawnPos);

            // Add it with the x, y position of BackGround
            Vector2 Pos = new Vector2(Player.transform.position.x + Random_x, Player.transform.position.y + Random_y);

            // Spawn Enemy at this position
            Instantiate<GameObject>(Enemy, Pos, Quaternion.identity);
            SpawnCount++;

            // SpawnDelay is refilled
            SpawnDelay = MaxSpawnDelay;
        }

        // Decrease the "SpawnCount" as the Enemy is Destroyed
        foreach (var item in _Enemy)
        {
            if (item.gameObject.GetComponent<EnemyFollow>().EnemyHealth <= 0)
            {
                SpawnCount--;
            }
        }

        SpawnDelay -= Time.deltaTime;
    }
}
