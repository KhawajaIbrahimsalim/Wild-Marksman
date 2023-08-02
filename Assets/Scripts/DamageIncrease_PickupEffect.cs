using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIncrease_PickupEffect : MonoBehaviour
{
    [SerializeField] private GameObject DamageIncreaseFire;

    [Header("P_Projectile Properties:")]
    [SerializeField] private GameObject P_SimpleProjectile;
    [SerializeField] private GameObject P_SpecialProjectile;

    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // If pick the DamageIncrease Pick-up again then only reset the delay time
            Player.GetComponent<PickUpItemSetUp>().DamageIncreaseActive_Delay = Player.GetComponent<PickUpItemSetUp>().Temp_DamageIncreaseActive_Delay;

            // Only one DamageIncrease Pick-up will be spawn at a time
            if (Player.GetComponent<PickUpItemSetUp>().DamageIncreaseFire == null)
            {
                // Store the Previous old P_ProjectileDamage value
                Player.GetComponent<PickUpItemSetUp>().P_SimpleProjectile_Damage = P_SimpleProjectile.GetComponent<DamageFor_P_Projectile>().ProjectileDamage;
                Player.GetComponent<PickUpItemSetUp>().P_SpecialProjectile_Damage = P_SpecialProjectile.GetComponent<DamageFor_P_Projectile>().ProjectileDamage;

                // Spawn the Fire effect
                GameObject _DamageIncreaseFire = Instantiate(DamageIncreaseFire);

                // Set Parent as Player
                _DamageIncreaseFire.transform.parent = Player.transform;

                // Set Local rotation to ZERO
                _DamageIncreaseFire.transform.localRotation = new Quaternion(0, 0, 0, 0);

                // Set Local position as ZERO to locate directly onto the Player
                _DamageIncreaseFire.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
    }
}
