using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathParticle;
    private bool playerFalled = false;

    private void Update()
    {
        if (!playerFalled && transform.position.y <= -1f)
        {
            playerFalled = true;
            deathParticle.Play();
            AudioManager.instance.PlaySound("Death");
            Invoke(nameof(Death), 1f);
        }
    }

    private void Death()
    {
        Utils.instance.RestartScene();
    }
}