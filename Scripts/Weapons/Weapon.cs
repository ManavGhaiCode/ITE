using UnityEngine;

public class Weapon : MonoBehaviour {
    public int bulletDamage = 5;
    public float bulletSpeed = 10f;
    public float bulletLifespan = 1f;
    public float TimeBetweenShots = .2f;
    public GameObject BulletPrefab;
    public Transform firePoint;

    private float _TimeBetweenShots = .2f;
    private float TimeToShoot;
    private bool isShooting = false;

    private Rigidbody2D rb;
    private GameObject Player;
    private Transform PlayerTransform;

    [SerializeField] private Vector2 Ofset;

    private void Start() {
        _TimeBetweenShots = TimeBetweenShots;
        TimeToShoot = Time.time + _TimeBetweenShots;

        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerTransform = Player.GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        isShooting = Input.GetMouseButton(0);

        int XPMP = 0;

        if (0 < PlayerTransform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x) {
            XPMP = -1;
        } else {
            XPMP = 1;
        }

        transform.position = (Vector2)PlayerTransform.position + (Ofset * XPMP);
    }

    private void FixedUpdate() {
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 Dir = MousePos - (Vector2)PlayerTransform.position;

        float angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        if (isShooting && Time.time >= TimeToShoot) {
            TimeToShoot = Time.time + _TimeBetweenShots;

            GameObject bullet = Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Bullet>().Damage = bulletDamage;
            bullet.GetComponent<Bullet>().lifespan = bulletLifespan;
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.right * bulletSpeed, ForceMode2D.Impulse);
        }
    }
}