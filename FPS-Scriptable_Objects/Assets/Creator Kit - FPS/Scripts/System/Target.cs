using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Target : MonoBehaviour
{
    public bool isTarget;
    public float health = 5.0f;
    public int pointValue;

    public Target_sObj SriptableTarget;
    public ParticleSystem DestroyedEffect;

    [Header("Audio")]
    public RandomPlayer HitPlayer;

    public AudioSource IdleSource;

    public bool Destroyed => m_Destroyed;

    private bool m_Destroyed = false;
    private float m_CurrentHealth;

    private void Awake()
    {
        if (isTarget)
        {
            Helpers.RecursiveLayerChange(transform, LayerMask.NameToLayer("Target"));
        }
        else
        {
            Helpers.RecursiveLayerChange(transform, LayerMask.NameToLayer("NonTarget"));
        }
        
    }

    private void Start()
    {
        LayerMask targetLayer = gameObject.layer;
        if (DestroyedEffect)
            PoolSystem.Instance.InitPool(DestroyedEffect, 16);
        GameParams currentParams = GameSystem.Instance.GetCurrentParams();
        if (currentParams != null)
        {
            if (currentParams.enemyParams != null && targetLayer == LayerMask.NameToLayer("Target"))
            {
                health = currentParams.enemyParams.health;
                pointValue = currentParams.enemyParams.pointValue;
            }
            if (currentParams.enemyParams != null && targetLayer == LayerMask.NameToLayer("NonTarget"))
            {
                health = currentParams.enemyParams.redHealth;
                pointValue = currentParams.enemyParams.redPointValue;
            }
        }
        m_CurrentHealth = health;
        if (IdleSource != null)
            IdleSource.time = Random.Range(0.0f, IdleSource.clip.length);
    }

    public void Got(float damage)
    {
        m_CurrentHealth -= damage;

        if (HitPlayer != null)
            HitPlayer.PlayRandom();

        if (m_CurrentHealth > 0)
            return;

        Vector3 position = transform.position;

        //the audiosource of the target will get destroyed, so we need to grab a world one and play the clip through it
        if (HitPlayer != null)
        {
            var source = WorldAudioPool.GetWorldSFXSource();
            source.transform.position = position;
            source.pitch = HitPlayer.source.pitch;
            source.PlayOneShot(HitPlayer.GetRandomClip());
        }

        if (DestroyedEffect != null)
        {
            var effect = PoolSystem.Instance.GetInstance<ParticleSystem>(DestroyedEffect);
            effect.time = 0.0f;
            effect.Play();
            effect.transform.position = position;
        }

        m_Destroyed = true;

        gameObject.SetActive(false);

        GameSystem.Instance.TargetDestroyed(pointValue);
    }
}