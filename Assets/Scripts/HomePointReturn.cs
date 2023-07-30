using UnityEngine;

public class HomePointReturn : MonoBehaviour
{
    [SerializeField] private float DistanceLimit;
    [SerializeField] private ParticleSystem ReSpawnParticle;

    private GameObject HomePoint;
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        HomePoint = GameObject.Find("Home Point");
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // When Player move away from the Home Point at a certain distance Re-locat it and Instantiate a Particle Effect
        if (Player)
        {
            float Distance = Vector2.Distance(HomePoint.transform.position, Player.transform.position);

            if (Distance > DistanceLimit)
            {
                Player.transform.position = HomePoint.transform.position;

                Instantiate(ReSpawnParticle, Player.transform.position, Player.transform.rotation);
            }
        }
    }
}
