using UnityEngine;

public class Enemy : MonoBehaviour {
    public float speed = 5f;
    
    protected Rigidbody2D rb;
    
    [SerializeField] protected int Heath;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int Damage) {
        Heath -= Damage;

        if (Heath <= 0) {
            Destroy(gameObject);
        }
    }
}