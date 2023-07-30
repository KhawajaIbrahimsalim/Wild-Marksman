using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float Projectile_Speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move Projectile every frame
        MoveProjectile();

        Destroy(gameObject, 2);
    }

    void MoveProjectile()
    {
        transform.position += transform.up * Time.deltaTime * Projectile_Speed;
    }
}
