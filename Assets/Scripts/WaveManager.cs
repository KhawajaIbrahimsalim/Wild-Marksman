using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Waves Properties:")]
    [SerializeField] private int WaveLimit;
    [SerializeField] private TextMeshProUGUI Wave_txt;
    [SerializeField] private float WaveTextShow_Delay;

    [Header("Count Down Timer Properties:")]
    public float WaveTime;
    [SerializeField] private TextMeshProUGUI Time_txt;

    [Header("Credit Properties:")]
    [SerializeField] private TextMeshProUGUI Credit_txt;
    [HideInInspector] public int Credit;

    [Header("Upgrade Properties:")]
    [SerializeField] private float UpgradeTime;
    [SerializeField] private GameObject GetReady_txt;
    [SerializeField] private GameObject UpgradeOptions_Panel;

    [Header("Boss Properties:")]
    [SerializeField] private GameObject Boss;
    [SerializeField] private GameObject BossHealthBar;

    [Header("Canvas Properties:")]
    [SerializeField] private GameObject Panel;
    [SerializeField] private GameObject NextLevel_btn;
    [SerializeField] private GameObject Retry_btn;
    [SerializeField] private GameObject PauseGame_Panel;
    [SerializeField] private GameObject P_SpecialAttack_btn;

    [Header("Kill Point Properties:")]
    [SerializeField] private long TotalPoints;
    [SerializeField] private long EnemyPoints;
    [SerializeField] private long BossPoints;
    [SerializeField] private GameObject PointsPanel;
    [SerializeField] private TextMeshProUGUI LevelSuccessStatus_txt;
    [SerializeField] private TextMeshProUGUI TotalPointsNum_txt;
    [SerializeField] private TextMeshProUGUI EnemyPointsNum_txt;
    [SerializeField] private TextMeshProUGUI BossPointsNum_txt;

    [Header("Reset Properties:")]
    [SerializeField] private GameObject P_SimpleProjectile;
    [SerializeField] private GameObject P_SpecialProjectile;

    [Header("Required Kills Properties:")]
    [SerializeField] private float RequiredKills;
    [SerializeField] private float NoOfKills;
    [SerializeField] private TextMeshProUGUI KillsNeeded_txt;

    [Header("Joystick Properties:")]
    [SerializeField] private GameObject joystick_Shoot;
    [SerializeField] private GameObject joystick_Move;
    [SerializeField] private RectTransform joystick_Shoot_Handle;
    [SerializeField] private RectTransform joystick_Move_Handle;

    private float minute;
    private float second;
    private int WaveCount;
    private float Temp_WaveTime;
    private float Temp_UpgradeTime;
    private GameObject Player;
    private bool IfTimeIsUp;
    private GameObject[] Remainig_Enemy;
    private float Temp_WaveTextShow_Delay;
    private float P_SimpleProjectile_ResetDamage;
    private float P_SpecialProjectile_ResetDamage;
    private bool IsKillsFullied;
    private bool IsPaused;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");

        WaveCount = 1;

        Temp_WaveTime = WaveTime;
        Temp_UpgradeTime = UpgradeTime;
        Temp_WaveTextShow_Delay = WaveTextShow_Delay;

        Credit = 0;
        IfTimeIsUp = false;
        IsKillsFullied = true;
        IsPaused = true;
        TotalPoints = 0;
        EnemyPoints = 0;
        BossPoints = 0;

        P_SimpleProjectile_ResetDamage = P_SimpleProjectile.GetComponent<DamageFor_P_Projectile>().ProjectileDamage;
        P_SpecialProjectile_ResetDamage = P_SpecialProjectile.GetComponent<DamageFor_P_Projectile>().ProjectileDamage;

        KillsNeeded_txt.text = "Kills Needed: " + NoOfKills + " / " + RequiredKills;
    }

    // Update is called once per frame
    void Update()
    {
        // If Player want to Pause the Game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Time.timeScale = 0;

                // Enable PausedGame_Panel
                PauseGame_Panel.SetActive(true);

                IsPaused = false;
            }

            else
            {
                Time.timeScale = 1;

                // Disable PausedGame_Panel
                PauseGame_Panel.SetActive(false);

                IsPaused = true;
            }
        }

        Remainig_Enemy = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var item in Remainig_Enemy)
        {
            if (item.GetComponent<EnemyFollow>().EnemyHealth <= 0)
            {
                // Also increase one Point
                EnemyPoints += 50;

                if (NoOfKills < RequiredKills && WaveCount <= WaveLimit - 1)
                {
                    // Increment NoOfKills
                    NoOfKills++;

                    // Show the Kills Needed
                    KillsNeeded_txt.text = "Kills Needed: " + NoOfKills + " / " + RequiredKills;
                }
            }
        }

        // YOU WIN!
        // If Boss is Defeated then what will happen
        if (Boss == null)
        {
            // Call BehaviourAfterWIN() function this will disable all the Important functionalities
            BehaviourAfterWIN();
        }

        // YOU LOSE!
        // If Player is Dead then what will happen
        if (Player == null)
        {
            // Call LoseBehaviour() function this will disable all the Important functionalities
            BehaviourAfterLOST();
        }

        if (WaveTextShow_Delay > 0.0f && IfTimeIsUp == false)
        {
            // Enable Wave_txt
            Wave_txt.enabled = true;

            // Show Wave count
            Wave_txt.text = "Wave " + WaveCount;
        }

        else
        {
            // Disable Wave_txt
            Wave_txt.enabled = false;
        }

        WaveTextShow_Delay -= Time.deltaTime;

        // From WAVE 1 - 4 there are Small Enemies that will Spawn
        if (WaveCount <= WaveLimit - 1)
        {
            // WaveTime will Start
            if (Time_txt != null && Player != null && IfTimeIsUp == false)
            {
                Time_txt.text = minute.ToString("0") + ":" + second.ToString("00");

                if (WaveTime <= 0.0f)
                {
                    // Increment 3 Credit if the 4th wave has ended
                    if (WaveCount == WaveLimit - 1)
                    {
                        Credit += 3;
                        // Show to HUD
                        Credit_txt.text = "Credit: " + Credit.ToString();
                    }

                    else
                    {
                        // Increment the Credit to use for the upgrade
                        Credit += 2;

                        // Show to HUD
                        Credit_txt.text = "Credit: " + Credit.ToString();
                    }

                    // WaveTime is Refilled
                    WaveTime = Temp_WaveTime;

                    // IfRimeIsUp is true which means 
                    IfTimeIsUp = true;
                }

                WaveTime -= Time.deltaTime;

                minute = ((int)WaveTime / 60);
                second = ((int)WaveTime % 60);
            }

            else // UpgradeTime will Start
            {
                if (Player != null)
                {
                    // Check if Killed the required kills
                    if (NoOfKills < RequiredKills)
                    {
                        // Call LoseBehaviour() function this will disable all the Important functionalities
                        BehaviourAfterLOST();

                        // Disable P_ProjectileSpawn
                        Player.GetComponent<P_ProjectileSpawn>().enabled = false;

                        // Stop the PlayerMovement
                        Player.GetComponent<PlayerMovement>().MoveSpeed = 0;

                        // And IsKillsFullied to false, mean Player failed to do the required kills
                        IsKillsFullied = false;
                    }

                    // Need Required kills to move to the next wave
                    if (IsKillsFullied == true)
                    {
                        minute = ((int)UpgradeTime / 60);
                        second = ((int)UpgradeTime % 60);

                        Time_txt.text = minute.ToString("0") + ":" + second.ToString("00");

                        UpgradeTime -= Time.deltaTime;

                        // Pre-Requirements:
                        {
                            // Destroy Remaining Enemies
                            foreach (var item in Remainig_Enemy)
                            {
                                Destroy(item);
                            }

                            // Disable EnemySpawning
                            gameObject.GetComponent<EnemySpawning>().enabled = false;

                            // Disable both Joyticks
                            joystick_Move.SetActive(false);
                            joystick_Shoot.SetActive(false);

                            // Disable P_ProjectileSpawn
                            Player.GetComponent<P_ProjectileSpawn>().enabled = false;

                            // Reset the position of the joystick handle
                            joystick_Shoot_Handle.localPosition = new Vector3(0, 0, 0);
                            joystick_Move_Handle.localPosition = new Vector3(0, 0, 0);

                            // Disable P_SpecialAttack_btn
                            P_SpecialAttack_btn.SetActive(false);

                            // Disable KillNeeded_txt
                            KillsNeeded_txt.enabled = false;

                            // Enable GetReady_txt
                            GetReady_txt.SetActive(true);

                            // Enable Upgrade option HUD
                            UpgradeOptions_Panel.SetActive(true);
                        }

                        if (UpgradeTime <= 0.0f)
                        {
                            // By making it false now the if condition will execute and not else, means 
                            // WaveTime will Start
                            IfTimeIsUp = false;

                            // Post-Requirements:
                            {
                                // Waves Count Increases
                                WaveCount++;

                                // Enable EnemySpawning
                                gameObject.GetComponent<EnemySpawning>().enabled = true;

                                // Enemy Count Limit is incresed for the next WAVE
                                gameObject.GetComponent<EnemySpawning>().SpawnLimit += 5;

                                // Spawn Count to 0, so we will reset the count
                                gameObject.GetComponent<EnemySpawning>().SpawnCount = 0.0f;

                                // WaveTextShow_Delay is Refilled
                                WaveTextShow_Delay = Temp_WaveTextShow_Delay;

                                // Enable both Joyticks
                                joystick_Move.SetActive(true);
                                joystick_Shoot.SetActive(true);

                                // Enable P_ProjectileSpawn
                                Player.GetComponent<P_ProjectileSpawn>().enabled = true;

                                // Disable P_SpecialAttack_btn
                                P_SpecialAttack_btn.SetActive(true);

                                // Disable GetReady_txt
                                GetReady_txt.SetActive(false);

                                // Enable KillNeeded_txt
                                KillsNeeded_txt.enabled = true;

                                // Disable Upgrade option HUD
                                UpgradeOptions_Panel.SetActive(false);

                                // Reset NoOfKills
                                NoOfKills = 0;

                                // Increase the RequiredKills
                                RequiredKills += 5;

                                // Show RequiredKills
                                KillsNeeded_txt.text = "Kills Needed: " + NoOfKills + " / " + RequiredKills;
                            }

                            // UpgradeTime is Refilled
                            UpgradeTime = Temp_UpgradeTime;
                        }
                    }  
                }
            }
        }

        else // WAVE 5 (THE BOSS BATTLE)
        {
            if (Boss != null)
            {
                // Disable Time_txt
                Time_txt.enabled = false;

                // Disable Required Kills
                KillsNeeded_txt.enabled = false;

                // Enable Boss itself in the scene
                Boss.SetActive(true);

                // Enable BOSS HealthBar
                BossHealthBar.SetActive(true);
            }
        }
    }

    private void BehaviourAfterLOST()
    {
        // Reset Projectile Damage Because Game is Ended
        P_SimpleProjectile.GetComponent<DamageFor_P_Projectile>().ProjectileDamage = P_SimpleProjectile_ResetDamage;
        P_SpecialProjectile.GetComponent<DamageFor_P_Projectile>().ProjectileDamage = P_SpecialProjectile_ResetDamage;

        // Disable GameController
        gameObject.SetActive(false);

        // Disable Panel
        Panel.SetActive(false);

        // Disable UpgradePanel
        UpgradeOptions_Panel.SetActive(false);

        // Enable PointsPanel
        PointsPanel.SetActive(true);

        // Enable Retry_btn
        Retry_btn.SetActive(true);

        // Disable NextLevel_btn
        NextLevel_btn.SetActive(false);

        // Change Text to "Faild to Survive"
        LevelSuccessStatus_txt.text = "Failed to Survive";

        // Add Both Enemy and Boss Points and store in "TotalPoints"
        TotalPoints = EnemyPoints + BossPoints;

        // Show Enemy Points
        EnemyPointsNum_txt.text = EnemyPoints.ToString();

        // show Boss Points
        BossPointsNum_txt.text = BossPoints.ToString();

        // Show Total Points
        TotalPointsNum_txt.text = TotalPoints.ToString();
    }

    private void BehaviourAfterWIN()
    {
        // Disable P_ProjectileSpawn
        Player.GetComponent<P_ProjectileSpawn>().enabled = false;

        // Stop the PlayerMovement
        Player.GetComponent<PlayerMovement>().MoveSpeed = 0;

        // Reset Projectile Damage Because Game is Ended
        P_SimpleProjectile.GetComponent<DamageFor_P_Projectile>().ProjectileDamage = P_SimpleProjectile_ResetDamage;
        P_SpecialProjectile.GetComponent<DamageFor_P_Projectile>().ProjectileDamage = P_SpecialProjectile_ResetDamage;

        // By killing the Boss Player will get 200 points
        BossPoints += 1000;

        // Destroy Remaining Enemies
        foreach (var item in Remainig_Enemy)
        {
            Destroy(item);
        }

        // Disable GameController
        gameObject.SetActive(false);

        // Disable Panel
        Panel.SetActive(false);

        // Disable UpgradePanel
        UpgradeOptions_Panel.SetActive(false);

        // Enable PointsPanel
        PointsPanel.SetActive(true);

        // Enable NextLevel_btn
        NextLevel_btn.SetActive(true);

        // Disable Retry_btn
        Retry_btn.SetActive(false);

        // Change Text to "Level # Completed"
        LevelSuccessStatus_txt.text = "Level 1 Completed";

        // Add Both Enemy and Boss Points and store in "TotalPoints"
        TotalPoints = EnemyPoints + BossPoints;

        // Show Enemy Points
        EnemyPointsNum_txt.text = EnemyPoints.ToString();

        // show Boss Points
        BossPointsNum_txt.text = BossPoints.ToString();

        // Show Total Points
        TotalPointsNum_txt.text = TotalPoints.ToString();
    }
}
