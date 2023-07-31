using UnityEngine;

public class PickUpItemSetUp : MonoBehaviour
{
    [Header("Shield (Pick-up item) Properties:")]
    public float ShieldActive_Delay;

    [HideInInspector] public GameObject Shield;
    [HideInInspector] public float Temp_ShieldActive_Delay;

    private float Player_Health;

    private void Start()
    {
        Temp_ShieldActive_Delay = ShieldActive_Delay;

        Player_Health = 0;
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