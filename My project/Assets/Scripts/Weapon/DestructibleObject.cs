using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    [Header("Destruction Settings")]
    [Tooltip("Should the object be destroyed immediately on hit?")]
    [SerializeField] private bool destroyOnHit = true;
    
    [Tooltip("Delay before destruction (in seconds)")]
    [SerializeField] private float destructionDelay = 0f;
    
    [Tooltip("Optional: particle effect on destruction")]
    [SerializeField] private GameObject destructionEffect;
    
    [Tooltip("Optional: sound effect on destruction")]
    [SerializeField] private AudioClip destructionSound;

    private bool isDestroyed = false;

    /// <summary>
    /// Destroy this object (can be called externally)
    /// </summary>
    public void Destroy()
    {
        if (isDestroyed) return;
        isDestroyed = true;

        // Spawn destruction effect if provided
        if (destructionEffect != null)
        {
            Instantiate(destructionEffect, transform.position, Quaternion.identity);
        }

        // Play destruction sound if provided
        if (destructionSound != null)
        {
            AudioSource.PlayClipAtPoint(destructionSound, transform.position);
        }

        // Destroy the object after delay
        if (destructionDelay > 0)
        {
            Destroy(gameObject, destructionDelay);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Check if object is already destroyed
    /// </summary>
    public bool IsDestroyed => isDestroyed;
}

