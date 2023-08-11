using UnityEngine;

public class Bullet : MonoBehaviour {
    public int Damage = 0;
    public float lifespan;

    [SerializeField] protected GameObject BulletHitEffect;

    private void Start() {
        Invoke("DestroyBullet", lifespan);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo) {
        DestroyBullet();
        Invoke("DestroyBullet", .05f);
    }

    private void DestroyBullet() {
        Instantiate(BulletHitEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}