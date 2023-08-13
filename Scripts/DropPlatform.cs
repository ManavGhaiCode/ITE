using UnityEngine;

public class DropPlatform : MonoBehaviour {
    private PlatformEffector2D Effector;
    private float waitTime = .15f;

    void Start() {
        Effector = GetComponent<PlatformEffector2D>();
    }

    void Update() {
        if (Input.GetKey("s")) {
            if (waitTime <= 0) {
                Effector.rotationalOffset = 180f;
            } else {
                waitTime -= Time.deltaTime;
            }
        } else {
            waitTime = .15f;
            Effector.rotationalOffset = 0f;
        }
    }
}
