using UnityEngine;

public class DamageFor_E_Projectile : MonoBehaviour
{
    [Header("Hit Properties:")]
    [SerializeField] private float ProjectileDamage;
    [SerializeField] private ParticleSystem HitParticle;

    [Header("Death Particle Effects:")]
    [SerializeField] private ParticleSystem DeathParticle;

    private bool IsHit = false;
    private bool IsDead = false;   
    private GameObject Player;

    private void Update()
    {
        if (IsHit)
        {
            // Instantiate the  Hit Particle
            Instantiate(HitParticle, transform.position, transform.rotation);

            IsHit = false;

            // Disable this projectile and so it can't be seen and the Particle Effect is inialized
            // before the object is destroyed.
            gameObject.SetActive(false);
        }

        if (IsDead)
        {
            // Instantiate the  Death Particle
            Instantiate(DeathParticle, transform.position, transform.rotation);

            IsDead = false;

            // Destroy Player
            Destroy(Player);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // For E_Projectile to damage the Player
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().PlayerHealth -= ProjectileDamage;

            if (other.gameObject.GetComponent<PlayerMovement>().PlayerHealth <= 0)
            {
                Player = other.gameObject;
                IsDead = true;
            }

            IsHit = true;
        }
    }
}
