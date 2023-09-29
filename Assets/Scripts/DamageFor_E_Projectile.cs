using EZCameraShake;
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
    private GameObject Audio_Source;

    private void Start()
    {
        Audio_Source = GameObject.Find("Audio Source");
    }

    private void Update()
    {
        if (IsHit)
        {
            if (CompareTag("Boss Impact Attack"))
            {
                // Play Audio
                Audio_Source.GetComponent<AudioController>().PlayAudio(Audio_Source.GetComponent<AudioController>().ElectroLaserAudio);

                // Instantiate the  Hit Particle
                ParticleSystem hitParticle = Instantiate(HitParticle, Player.transform.position, Player.transform.rotation);

                hitParticle.transform.SetParent(Player.transform);

                IsHit = false;

                Debug.Log("Here");
            }

            else
            {
                // Instantiate the  Hit Particle
                Instantiate(HitParticle, transform.position, transform.rotation);

                IsHit = false;

                Destroy(gameObject, 1f);

                // Disable this projectile and so it can't be seen and the Particle Effect is inialized
                // before the object is destroyed.
                gameObject.SetActive(false);
            }
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
        if (other.CompareTag("Player"))
        {
            // if Shield is ON then don't deduct the players health
            if (other.gameObject.GetComponent<PickUpItemSetUp>().IsShieldActive == false)
            {
                other.gameObject.GetComponent<PlayerMovement>().PlayerHealth -= ProjectileDamage;
            }

            Player = other.gameObject;

            if (other.gameObject.GetComponent<PlayerMovement>().PlayerHealth <= 0)
            {
                // Play Audio
                Audio_Source.GetComponent<AudioController>().PlayAudio(Audio_Source.GetComponent<AudioController>().DestroyAudio);

                // Then Camera Shake
                CameraShake(2f, 2f);

                // Then Destroy in Update()
                IsDead = true;
            }

            if (CompareTag("Boss_Special Attack"))
            {
                // Play Audio
                Audio_Source.GetComponent<AudioController>().PlayAudio(Audio_Source.GetComponent<AudioController>().SifiAudio);

                // Then Camera Shake
                CameraShake(4f, 4f);
            }

            IsHit = true;
        }
    }

    private void CameraShake(float Magnitude, float Roughness)
    {
        CameraShaker.Instance.ShakeOnce(Magnitude, Roughness, .1f, 1f);
    }
}
