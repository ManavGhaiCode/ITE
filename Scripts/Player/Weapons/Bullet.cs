using UnityEngine;

public class Bullet : MonoBehaviour {
    public int Damage = 0;
    public float lifespan;

    [SerializeField] protected GameObject BulletHitEffect;

    private void Start() {
        Invoke("DestroyBullet", lifespan);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo) {
        Enemy enemy = hitInfo.GetComponent<Enemy>();

        if (enemy != null) {
            enemy.TakeDamage(Damage);
        }

        DestroyBullet();
        Invoke("DestroyBullet", .05f);
    }

    private void DestroyBullet() {
        GameObject Effect = Instantiate(BulletHitEffect, transform.position, Quaternion.identity);
        Effect.transform.localScale = new Vector3 (.7f, .7f, .7f);
        Destroy(gameObject);
    }
}