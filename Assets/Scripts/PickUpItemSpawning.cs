using UnityEngine;

public class PickUpItemSpawning : MonoBehaviour
{
    [Header("Pick-up items Properties:")]
    [SerializeField] private GameObject[] PickUpItems;
    [SerializeField] private float SpawnPos;
    [SerializeField] private float SpawnDelay;

    [Header("Pick-up Destroy Properties:")]
    [SerializeField] float PickUp_Destroy_Distance;

    private GameObject Player;
    private GameObject[] ToDestroyItems;
    private float Temp_SpawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");                                         

        Temp_SpawnDelay = SpawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        // To Store Pick-up item on the scene
        ToDestroyItems = GameObject.FindGameObjectsWithTag("PickUp");

        if (Player)
        {
            if (SpawnDelay <= 0.0f)
            {
                // Choose a Pick-up item to spawn
                int ChoosePickUpItems = Random.Range(0, PickUpItems.Length);

                // Set Random Values of x and y
                float Random_x = Random.Range(-SpawnPos, SpawnPos);
                float Random_y = Random.Range(-SpawnPos, SpawnPos);

                // Set position to spawn
                Vector2 Pos = new Vector2(Player.transform.position.x + Random_x, Player.transform.position.y + Random_y);

                // spawn the Pick-up item at a position
                Instantiate(PickUpItems[ChoosePickUpItems], Pos, Quaternion.identity);

                // Refill the delay
                SpawnDelay = Temp_SpawnDelay;
            }

            SpawnDelay -= Time.deltaTime;

            // To Destroy Pick-up item when Player is far-apart from that Pick-up item
            foreach (var PickUpItem in ToDestroyItems)
            {
                if (PickUpItem != null)
                {
                    Vector2 Dis = PickUpItem.transform.position - Player.transform.position;

                    if (Dis.magnitude > PickUp_Destroy_Distance)
                    {
                        DestroyImmediate(obj: PickUpItem, allowDestroyingAssets: true);
                    }
                }
            }
        }
    }
}
