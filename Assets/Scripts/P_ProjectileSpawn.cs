using UnityEngine;

public class P_ProjectileSpawn : MonoBehaviour
{
    [SerializeField] private GameObject P_SimpleProjectile;
    [SerializeField] private GameObject SpecialProjectile;
    [SerializeField] private GameObject Projectile_SpawnPoint;
    [SerializeField] private float DelayFor_SpecialAttack;
    [SerializeField] private Animator SpecialAttack_ColorChange;
    [SerializeField] private Animator Gun_SpecialAttack_ColorChange;

    [Header("Joystick Properties:")]
    public FloatingJoystick joystick;
    [SerializeField] private float DelayFor_SimpleAttack;
    [SerializeField] private RectTransform joystick_Shoot_Handle;

    private float DelayFor_SpecialAttack_temp;
    private float DelayFor_SimpleAttack_temp;
    private bool IsPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        DelayFor_SpecialAttack_temp = DelayFor_SpecialAttack;

        DelayFor_SimpleAttack_temp = DelayFor_SimpleAttack;
    }

    // Update is called once per frame
    void Update()
    {
        float shootHorizontal = joystick.Horizontal;
        float shootVertical = joystick.Vertical;

        // Simple Attack

        // For PC
        //if (Input.GetButtonDown("Fire1") && P_SimpleProjectile != null)
        //{
        //    SimpleShoot();
        //}

        // For Mobile
        if (joystick_Shoot_Handle.localPosition != new Vector3(0, 0, 0))
        {
            if (shootHorizontal != 0.0f || shootVertical != 0.0f && P_SimpleProjectile != null)
            {
                if (DelayFor_SimpleAttack <= 0.0f)
                {
                    SimpleShoot();

                    DelayFor_SimpleAttack = DelayFor_SimpleAttack_temp;
                }
            }
        }

        DelayFor_SimpleAttack -= Time.deltaTime;

        // IN FURTURE: I will add an enery system in which you have to gain energy by killing enemy Droids and at certain amount of energy
        // you gain you would be able to use the "Special Attack"

        // Special Attack
        if (IsPressed && DelayFor_SpecialAttack <= 0 && SpecialProjectile != null /*|| Input.GetKeyUp("mouse 1") && DelayFor_SpecialAttack <= 0 && SpecialProjectile != null*/)
        {
            SpecialAttack();

            // If the Special projectile is shot now Delay will also refill
            DelayFor_SpecialAttack = DelayFor_SpecialAttack_temp;

            // When Special Attack Animation ENDS
            Gun_SpecialAttack_ColorChange.SetBool("IsIdol", true);
            SpecialAttack_ColorChange.SetBool("IsIdol", true);
        }

        else if (IsPressed /* || Input.GetKey("mouse 1")*/)
        {
            DelayFor_SpecialAttack -= Time.deltaTime;

            // When Special Attack Animation STARTS
            Gun_SpecialAttack_ColorChange.SetBool("IsIdol", false);
            SpecialAttack_ColorChange.SetBool("IsIdol", false);
        }

        else if (!IsPressed && DelayFor_SpecialAttack > 0 && DelayFor_SpecialAttack < DelayFor_SpecialAttack_temp /*|| !Input.GetKey("mouse 1") && DelayFor_SpecialAttack > 0 && DelayFor_SpecialAttack < DelayFor_SpecialAttack_temp*/)
        {
            // If Click is not held and also Delay is greater(>) than 0, then the Delay will reset itself
            DelayFor_SpecialAttack = DelayFor_SpecialAttack_temp;

            // When Special Attack Animation STARTS
            Gun_SpecialAttack_ColorChange.SetBool("IsIdol", true);
            SpecialAttack_ColorChange.SetBool("IsIdol", true);
        }
        //
    }

    void SimpleShoot()
    {
        Instantiate<GameObject>(P_SimpleProjectile, Projectile_SpawnPoint.transform.position, transform.rotation);
    }

    void SpecialAttack()
    {
        Instantiate<GameObject>(SpecialProjectile, Projectile_SpawnPoint.transform.position, transform.rotation);
    }

    public void OnButtonDown()
    {
        IsPressed = true;
    }

    public void OnButtonUp()
    {
        IsPressed = false;
    }
}