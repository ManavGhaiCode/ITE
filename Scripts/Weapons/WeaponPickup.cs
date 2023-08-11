using UnityEngine;

public class WeaponPickup : MonoBehaviour {
    public GameObject WeaponRef;

    private void OnTriggerEnter2D(Collider2D hitInfo) {
        Player player = hitInfo.GetComponent<Player>();

        if (player != null) {
            player.TakeWeapon(WeaponRef);
            Destroy(gameObject);
        }
    }
}