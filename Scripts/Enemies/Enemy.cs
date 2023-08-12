using UnityEngine;

public class Enemy : MonoBehaviour {
    public float speed = 5f;
    
    protected Rigidbody2D rb;
    protected Transform PlayerTransform;
    
    [SerializeField] protected int Heath;
    [SerializeField] protected int Damage;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void TakeDamage(int Damage) {
        Heath -= Damage;

        if (Heath <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D hitInfo) {
        Player player = hitInfo.gameObject.GetComponent<Player>();

        if (player != null) {
            int Dir = 0;
            
            if (PlayerTransform.position.x > transform.position.x) {
                Dir = 1;
            } else if (PlayerTransform.position.x < transform.position.x) {
                Dir = -1;
            }

            player.Knockback(Dir);
            player.TakeDamage(Damage);
        }
    }
}