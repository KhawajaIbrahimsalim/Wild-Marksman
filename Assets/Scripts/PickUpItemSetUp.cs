using TMPro;
using UnityEngine;

public class PickUpItemSetUp : MonoBehaviour
{
    [Header("UI Customization Properties:")]
    [SerializeField] GameObject PickItems_Default_Position;

    [Header("Shield (Pick-up item) Properties:")]
    public float ShieldActive_Delay;
    [SerializeField] private GameObject ShieldActive_Delay_txt;

    [HideInInspector] public GameObject Shield;
    [HideInInspector] public float Temp_ShieldActive_Delay;

    private float Player_Health;

    [Header("Damage Increase (Pick-up item) Properties:")]
    public float DamageIncreaseActive_Delay;
    [SerializeField] private float IncreasedTimesDamage;
    [SerializeField] private GameObject DamageIncreaseActive_Delay_txt;

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

        // For Shield Pick-up item
        if (Shield != null)
        {
            GetComponent<PlayerMovement>().PlayerHealth = Player_Health;

            ShieldActive_Delay -= Time.deltaTime;

            // If DamageIncreaseActive_Delay_txt is already active then ShieldActive_Delay_txt will locate under it
            if (DamageIncreaseActive_Delay_txt.active == true && ShieldActive_Delay_txt.active == false)
            {
                ShieldActive_Delay_txt.transform.position = new Vector2(ShieldActive_Delay_txt.transform.position.x, ShieldActive_Delay_txt.transform.position.y - 90);
            }

            // If DamageIncreaseActive_Delay_txt is now inactive then ShieldActive_Delay_txt will locate to the default position (or position of the DamageIncreaseActive_Delay_txt)
            else if (DamageIncreaseActive_Delay_txt.active == false && ShieldActive_Delay_txt.active == true && ShieldActive_Delay_txt.transform.position.y == PickItems_Default_Position.transform.position.y - 90)
            {
                ShieldActive_Delay_txt.transform.position = new Vector2(ShieldActive_Delay_txt.transform.position.x, ShieldActive_Delay_txt.transform.position.y + 90);
            }

            ShieldActive_Delay_txt.SetActive(true);

            ShieldActive_Delay_txt.GetComponent<TextMeshProUGUI>().text = ShieldActive_Delay.ToString("0");

            if (ShieldActive_Delay <= 0.0f)
            {
                // Destroy Shield
                Destroy(Shield);

                // Refill ShieldActive_Delay
                ShieldActive_Delay = Temp_ShieldActive_Delay;

                // To Set ShieldActive_Delay_txt to the Default position if it moved from it
                if (ShieldActive_Delay_txt.transform.position.y == PickItems_Default_Position.transform.position.y - 90)
                {
                    ShieldActive_Delay_txt.transform.position = new Vector2(ShieldActive_Delay_txt.transform.position.x, ShieldActive_Delay_txt.transform.position.y + 90);
                }

                // Disable ShieldActive_Delay_txt
                ShieldActive_Delay_txt.SetActive(false);
            }
        }

        // Find DamageIncreaseFire effect in the scene
        DamageIncreaseFire = GameObject.Find("Damage Increase Fire(Clone)");

        // For DamageIncreaseFire Pick-up item
        if (DamageIncreaseFire != null)
        {
            DamageIncreaseActive_Delay -= Time.deltaTime;

            // If ShieldActive_Delay_txt is already active then DamageIncreaseActive_Delay_txt will locate under it
            if (ShieldActive_Delay_txt.active == true && DamageIncreaseActive_Delay_txt.active == false)
            {
                DamageIncreaseActive_Delay_txt.transform.position = new Vector2(DamageIncreaseActive_Delay_txt.transform.position.x, DamageIncreaseActive_Delay_txt.transform.position.y - 90);
            }

            // If ShieldActive_Delay_txt is now inactive then DamageIncreaseActive_Delay_txt will locate to the default position (or position of the ShieldActive_Delay_txt)
            else if (ShieldActive_Delay_txt.active == false && DamageIncreaseActive_Delay_txt.active == true && DamageIncreaseActive_Delay_txt.transform.position.y == PickItems_Default_Position.transform.position.y - 90)
            {
                DamageIncreaseActive_Delay_txt.transform.position = new Vector2(DamageIncreaseActive_Delay_txt.transform.position.x, DamageIncreaseActive_Delay_txt.transform.position.y + 90);
            }

            DamageIncreaseActive_Delay_txt.SetActive(true);

            DamageIncreaseActive_Delay_txt.GetComponent<TextMeshProUGUI>().text = DamageIncreaseActive_Delay.ToString("0");

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

                // To Set DamageIncreaseActive_Delay_txt to the Default position if it moved from it
                if (DamageIncreaseActive_Delay_txt.transform.position.y == PickItems_Default_Position.transform.position.y - 90)
                {
                    DamageIncreaseActive_Delay_txt.transform.position = new Vector2(DamageIncreaseActive_Delay_txt.transform.position.x, DamageIncreaseActive_Delay_txt.transform.position.y + 90);
                }

                DamageIncreaseActive_Delay_txt.SetActive(false);

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