using UnityEngine;
using UnityEngine.UI;

public class EnemyFollow : MonoBehaviour
{
    [Header("Health Properties:")]
    public float EnemyHealth;
    [SerializeField] private Slider BossHealthBar;

    [Header("Simple Attack Properties:")]
    [SerializeField] private GameObject E_SimpleProjectile;
    [SerializeField] private GameObject[] E_ProjectileSpawnPoint;
    [SerializeField] private float FireDelay;

    [Header("Boss Simple and Mines Attack Properties:")]
    [SerializeField] float bitDelay;
    [SerializeField] private GameObject E_Mine;
    [SerializeField] private GameObject[] MinesSpawnPoint;
    [SerializeField] private float MineSpawnDelay;

    private float Temp_MineSpawnDelay;
    private float Temp_FireDelay;
    private float Temp_bitDelay;
    // Summary:
    // Stores the number of shots the Boss enemy shoot.
    private float CountFire;


    [Header("Follow Properties:")]
    public float MaxSpeed;
    public float MinSpeed;
    public float FollowSpeed;
    public float Boss_FollowSpeed;

    [SerializeField] private float Offset;

    private GameObject Player;
    private float EnemyToPlayer_Distance;
    private float BossMaxHealth;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        FollowSpeed = Random.Range(MinSpeed, MaxSpeed);

        if (tag == "Boss")
        {
            FollowSpeed = Boss_FollowSpeed;
        }

        Temp_FireDelay = FireDelay;
        Temp_bitDelay = bitDelay;
        Temp_MineSpawnDelay = MineSpawnDelay;

        BossMaxHealth = EnemyHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Show Boss Health
        if (tag == "Boss")
        {
            BossHealthBar.value = EnemyHealth / BossMaxHealth;
        }

        if (Player)
        {
            // Calculate Distance from Enemy to the Player
            EnemyToPlayer_Distance = Vector2.Distance(transform.position, Player.transform.position);

            // Set a direction
            Vector2 Direction = Player.transform.position - transform.position;
            Direction.Normalize();

            if (EnemyToPlayer_Distance > Offset)
            {
                // Move Enemy towards the Target Direction
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, FollowSpeed * Time.deltaTime);
                // Rotate towards the Target direction
                transform.up = Direction * Time.deltaTime;
            }

            else
            {
                // Only Rotate towards the Target direction
                transform.up = Direction * Time.deltaTime;

                // Simple Attack
                if (E_SimpleProjectile != null && FireDelay <= 0.0f)
                {
                    // Enemy_Boss
                    if (this.gameObject.tag == "Boss")
                    {
                        if (bitDelay <= 0.0f && FireDelay <= 0.0f) // Delay between each shot after main Delay (means: Delay inside a Delay)
                        {
                            // Instantiate a bullet
                            SimpleShoot();
                            
                            // Refill the bitDelay
                            bitDelay = Temp_bitDelay;
                        }

                        bitDelay -= Time.deltaTime;

                        if (CountFire >= 3)
                        {
                            // Refill the FireDelay
                            FireDelay = Temp_FireDelay;

                            // Reset the CountFire
                            CountFire = 0.0f;
                        }
                    }
                    // Enemy
                    else
                    {
                        // Instantiate a bullet
                        SimpleShoot();

                        // Refill the FireDelay
                        FireDelay = Temp_FireDelay;
                    }
                }

                // Description: If the Enemy is in the required range of the Player, the Enemy will stop (but can rotate)
                // and will be able to fire at the Player.
            }

            // Mines Attack
            if (this.gameObject.tag == "Boss")
            {
                if (MineSpawnDelay <= 0.0f)
                {
                        MinesAttack();

                    // Refill the MineSpawnDelay
                    MineSpawnDelay = Temp_MineSpawnDelay;
                }
            }

            MineSpawnDelay -= Time.deltaTime;

            FireDelay -= Time.deltaTime;
        }
    }

    void SimpleShoot()
    {
        foreach (var ProjectileSpawnPoint in E_ProjectileSpawnPoint)
        {
            Instantiate(E_SimpleProjectile, ProjectileSpawnPoint.transform.position, transform.rotation);
        }

        // Count each shot
        CountFire++;
    }

    void MinesAttack()
    {
        foreach (var Mines_SpawnPoint in MinesSpawnPoint)
        {
            Instantiate(E_Mine, Mines_SpawnPoint.transform.position, transform.rotation);
        }
    }
}