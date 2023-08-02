using UnityEngine;

public class PickUpItemSetUp : MonoBehaviour
{
    [Header("Shield (Pick-up item) Properties:")]
    public float ShieldActive_Delay;

    [HideInInspector] public GameObject Shield;
    [HideInInspector] public float Temp_ShieldActive_Delay;

    private float Player_Health;

    [Header("Damage Increase (Pick-up item) Properties:")]
    public float DamageIncreaseActive_Delay;
    [SerializeField] private float IncreasedTimesDamage;

    [HideInInspector] public GameObject DamageIncreaseFire;
    [HideInInspector] public float Temp_DamageIncreaseActive_Delay;

    [Header("P_Projectile Properties:")]
    [SerializeField] private GameObject P_SimpleProjectile;
    [SerializeField] private GameObject P_SpecialProjectile;

    [HideInInspector] public float P_SimpleProjectile_Damage;
    [HideInInspector] public float P_SpecialProjectile_Damage;

    private bool IsDelayStarted;

    private void Start()
    {
        Temp_ShieldActive_Delay = ShieldActive_Delay;
        Temp_DamageIncreaseActive_Delay = DamageIncreaseActive_Delay;

        Player_Health = 0;

        IsDelayStarted = true;
    }

    [System.Obsolete]
    private void Update()
    {
        // Find Shield in the scene
        Shield = GameObject.Find("Shield(Clone)");

        if (Shield != null)
        {
            GetComponent<PlayerMovement>().PlayerHealth = Player_Health;

            ShieldActive_Delay -= Time.deltaTime;

            if (ShieldActive_Delay <= 0.0f)
            {
                // Destroy Shield
                Destroy(Shield);

                // Refill ShieldActive_Delay
                ShieldActive_Delay = Temp_ShieldActive_Delay;
            }
        }

        // Find DamageIncreaseFire effect in the scene
        DamageIncreaseFire = GameObject.Find("Damage Increase Fire(Clone)");

        if (DamageIncreaseFire != null)
        {
            DamageIncreaseActive_Delay -= Time.deltaTime;

            if (DamageIncreaseActive_Delay > 0 && IsDelayStarted)
            {
                // Increase the Damage
                P_SimpleProjectile.GetComponent<DamageFor_P_Projectile>().ProjectileDamage *= IncreasedTimesDamage;
                P_SpecialProjectile.GetComponent<DamageFor_P_Projectile>().ProjectileDamage *= IncreasedTimesDamage;      

                IsDelayStarted = false;
            }

            if (DamageIncreaseActive_Delay <= 0)
            {
                // Reset Damage value back to normal
                P_SimpleProjectile.GetComponent<DamageFor_P_Projectile>().ProjectileDamage = P_SimpleProjectile_Damage;
                P_SpecialProjectile.GetComponent<DamageFor_P_Projectile>().ProjectileDamage = P_SpecialProjectile_Damage;

                // Destroy the Fire effect
                Destroy(DamageIncreaseFire);

                // Refill the DamageIncreaseActive_Delay
                DamageIncreaseActive_Delay = Temp_DamageIncreaseActive_Delay;

                IsDelayStarted = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUp"))
        {
            // Set Last Player Health before Shield Pick up
            Player_Health = GetComponent<PlayerMovement>().PlayerHealth;

            // As Player Collides with Pick-up items it destroys the Pick-up item
            Destroy(collision.gameObject);
        }
    }
}