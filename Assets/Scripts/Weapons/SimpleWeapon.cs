using UnityEngine;

public class SimpleWeapon : BaseWeapon {
    public override void OnShootDown (Vector3 fireVec, Transform playerTransform) {
        Shoot (fireVec, playerTransform);
    }

    public override void OnShootUp (Vector3 fireVec, Transform playerTransform) {

    }
}