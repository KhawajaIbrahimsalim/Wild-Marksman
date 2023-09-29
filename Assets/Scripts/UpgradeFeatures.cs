using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeFeatures : MonoBehaviour
{
    [Header("Important Features:")]
    [SerializeField] private GameObject[] AT_UpgradeSticks;
    [SerializeField] private GameObject[] MH_UpgradeSticks;
    [SerializeField] private GameObject[] H_UpgradeSticks;
    [SerializeField] private GameObject P_SimpleProjectile;
    [SerializeField] private GameObject P_SpecialProjectile;
    [SerializeField] private int AT_Count;
    [SerializeField] private int MH_Count;
    [SerializeField] private int H_Count;
    [SerializeField] private GameObject Upgradepanel;

    [Header("Upgrade Values:")]
    [SerializeField] private float P_SimpleATTACK_UP;
    [SerializeField] private float P_SpecialATTACK_UP;
    [SerializeField] private float MaxHealth_UP;
    [SerializeField] private float HealPercentage;

    [Header("Credit Properties:")]
    [SerializeField] private TextMeshProUGUI Credit_txt;
    [SerializeField] private GameObject NotEnoughCredit;
    [SerializeField] private float NotEnoughCredit_Delay;


    private GameObject Player;
    private float Temp_NotEnoughCredit_Delay;
    private bool IsNotEnoughCredit = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");

        Upgradepanel.SetActive(true);

        AT_UpgradeSticks = GameObject.FindGameObjectsWithTag("AT");
        MH_UpgradeSticks = GameObject.FindGameObjectsWithTag("MH");
        H_UpgradeSticks = GameObject.FindGameObjectsWithTag("H");

        Upgradepanel.SetActive(false);

        Temp_NotEnoughCredit_Delay = NotEnoughCredit_Delay;
    }

    private void Update()
    {
        if (IsNotEnoughCredit && NotEnoughCredit_Delay > 0.0f)
        {
            NotEnoughCredit.SetActive(true);
        }

        else
        {
            NotEnoughCredit.SetActive(false);

            // IsNotEnoughCredit make it false
            IsNotEnoughCredit = false;
        }

        NotEnoughCredit_Delay -= Time.deltaTime;
    }

    private Color UpgradeStickColor()
    {
        float r = 255.0f / 255.0f;
        float g = 180.0f / 255.0f;
        float b = 0.0f / 255.0f;
        float a = 255.0f / 255.0f;

        var newColor = new Color(r, g, b, a);

        return newColor;
    }

    // For ATTACK
    public void Attack()
    {
        // As long as the Count is less than Sticks Attack will be increased
        if (AT_Count < AT_UpgradeSticks.Length && gameObject.GetComponent<WaveManager>().Credit >= 2)
        {
            // Increase Damage
            P_SimpleProjectile.GetComponent<DamageFor_P_Projectile>().ProjectileDamage += P_SimpleATTACK_UP;
            P_SpecialProjectile.GetComponent<DamageFor_P_Projectile>().ProjectileDamage += P_SpecialATTACK_UP;

            // Add Color to the Stick if Button is pressed
            AT_UpgradeSticks[AT_Count++].GetComponent<RawImage>().color = UpgradeStickColor();

            // Decrement Credit because now they are used
            gameObject.GetComponent<WaveManager>().Credit -= 2;

            // Show the change Value of the credit
            Credit_txt.text = "Credit: " + gameObject.GetComponent<WaveManager>().Credit;
        }

        else if (gameObject.GetComponent<WaveManager>().Credit < 2)
        {
            // Refill the NotEnoughCredit_Delay
            NotEnoughCredit_Delay = Temp_NotEnoughCredit_Delay;

            // IsNotEnoughCredit make it true
            IsNotEnoughCredit = true;
        }
    }

    // For MAX HEALTH
    public void MaxHealth()
    {
        if (Player)
        {
            // As long as the Count is less than Sticks Max Health will be increased
            if (MH_Count < MH_UpgradeSticks.Length && gameObject.GetComponent<WaveManager>().Credit >= 1)
            {
                // Increase Max Health
                Player.GetComponent<PlayerMovement>().PlayerMaxHealth += MaxHealth_UP;

                // Add Color to the Stick if Button is pressed
                MH_UpgradeSticks[MH_Count++].GetComponent<RawImage>().color = UpgradeStickColor();

                // Decrement Credit because now they are used
                gameObject.GetComponent<WaveManager>().Credit -= 1;

                // Show the change Value of the credit
                Credit_txt.text = "Credit: " + gameObject.GetComponent<WaveManager>().Credit;
            }

            else if (gameObject.GetComponent<WaveManager>().Credit < 1)
            {
                // Refill the NotEnoughCredit_Delay
                NotEnoughCredit_Delay = Temp_NotEnoughCredit_Delay;

                // IsNotEnoughCredit make it true
                IsNotEnoughCredit = true;
            }
        }
    }

    public void Heal()
    {
        if (Player)
        {
            // Calculating the specified Precentage value from the PlayerHealth
            float HealValue = Player.GetComponent<PlayerMovement>().PlayerMaxHealth / 100 * HealPercentage;
            Debug.Log(HealValue);

            // As long as the Count is less than Sticks Player will Heal
            if (H_Count < H_UpgradeSticks.Length && gameObject.GetComponent<WaveManager>().Credit >= 1)
            {
                if (Player.GetComponent<PlayerMovement>().PlayerHealth < Player.GetComponent<PlayerMovement>().PlayerMaxHealth)
                {
                    // Minus the Max Health by Current Health of the player to get health which is missing from the total
                    float HealthNeeded = Player.GetComponent<PlayerMovement>().PlayerMaxHealth - Player.GetComponent<PlayerMovement>().PlayerHealth;

                    if (HealthNeeded < HealValue)
                    {
                        // Then Add it in Player Current Health
                        Player.GetComponent<PlayerMovement>().PlayerHealth += HealthNeeded;
                    }

                    else
                    {
                        // Then Add it in Player Current Health
                        Player.GetComponent<PlayerMovement>().PlayerHealth += HealValue;
                    }

                    // Add Color to the Stick if Button is pressed
                    H_UpgradeSticks[H_Count++].GetComponent<RawImage>().color = UpgradeStickColor();

                    // Decrement Credit because now they are used
                    gameObject.GetComponent<WaveManager>().Credit -= 1;

                    // Show the change Value of the credit
                    Credit_txt.text = "Credit: " + gameObject.GetComponent<WaveManager>().Credit;
                }
            }

            else if (gameObject.GetComponent<WaveManager>().Credit < 1)
            {
                // Refill the NotEnoughCredit_Delay
                NotEnoughCredit_Delay = Temp_NotEnoughCredit_Delay;

                // IsNotEnoughCredit make it true
                IsNotEnoughCredit = true;
            }
        }
    }
}
