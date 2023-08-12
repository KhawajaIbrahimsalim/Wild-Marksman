using UnityEngine;

public class DamageFor_P_Projectile : MonoBehaviour
{
    [Header("Hit Properties:")]
    public float ProjectileDamage;
    [SerializeField] private ParticleSystem HitParticle;

    [Header("Death Particle Effects:")]
    [SerializeField] private ParticleSystem Enemy_DeathParticle;
    [SerializeField] private ParticleSystem Boss_DeathParticle;

    private bool IsHit = false;
    private bool IsDead = false;
    private GameObject Enemy;
    private ParticleSystem DeathParticle;

    private void Update()
    {
        if (IsHit)
        {
            Instantiate(HitParticle, transform.position, transform.rotation);

            IsHit = false;

            gameObject.SetActive(false);
        }

        if (IsDead)
        {
            Instantiate(DeathParticle, transform.position, transform.rotation);

            IsDead = false;

            Destroy(Enemy);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Set Particle System according to tag
        if (other.CompareTag("Boss"))
        {
            DeathParticle = Boss_DeathParticle;
        }

        else if (other.CompareTag("Enemy"))
        {
            DeathParticle = Enemy_DeathParticle;
        }

        // For P_Projectile to damage the Enemy
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<EnemyFollow>().EnemyHealth -= ProjectileDamage;

            if (other.gameObject.GetComponent<EnemyFollow>().EnemyHealth <= 0)
            {
                Enemy = other.gameObject;
                IsDead = true;
            }

            IsHit = true;
        }
    }
}
