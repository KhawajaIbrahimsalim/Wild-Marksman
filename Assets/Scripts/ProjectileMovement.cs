using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float Projectile_Speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move Projectile every frame
        MoveProjectile();

        Destroy(gameObject, 2);
    }

    void MoveProjectile()
    {
        Vector2 DirectionalForce = Projectile_Speed * Time.deltaTime * transform.up;

        transform.Translate(DirectionalForce, Space.World);
    }
}
