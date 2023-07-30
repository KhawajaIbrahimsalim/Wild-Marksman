using UnityEngine;

public class VoidSpawning : MonoBehaviour
{
    [Header("Spawn Void:")]
    [SerializeField] private GameObject Void;
    [SerializeField] private float SpawnPos;
    [SerializeField] private SpriteRenderer BackGround_SR;
    [SerializeField] private int SpawnLimit;

    [Header("Void Destroy Distance: ")]
    [SerializeField] private float Void_Destroy_Distance;
    [SerializeField] private GameObject[] _void;

    private GameObject Player;
    private float SpawnCount;

    // Start is called before the first frame update
    void Start()
    {
        SpawnCount = 0;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _void = GameObject.FindGameObjectsWithTag("Void");

        if (Player && SpawnCount < SpawnLimit)
        {
            // Get a Random x and y value
            float Random_x = Random.Range(-SpawnPos, SpawnPos);
            float Random_y = Random.Range(-SpawnPos, SpawnPos);

            // Add it with the x, y position of BackGround
            Vector2 Pos = new Vector2(Player.transform.position.x + Random_x, Player.transform.position.y + Random_y);

            // Spawn Enemy at this position
            Instantiate<GameObject>(Void, Pos, Quaternion.identity);

            SpawnCount++;
        }

        // If Player moves away from the Void trap at a certain distance then destroy the Void
        if (Player != null)
        {
            foreach (GameObject item in _void)
            {
                if (item != null)
                {
                    Vector2 Dis = item.transform.position - Player.transform.position;

                    if (Dis.magnitude > Void_Destroy_Distance)
                    {
                        Destroy(item);
                        SpawnCount--;
                    }
                }
            }
        }
    }
}
