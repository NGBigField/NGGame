using UnityEngine;

public class SimpleWeapon : BaseWeapon {
    public override void OnShootDown (Vector3 fireVec) {
        Shoot (fireVec);
    }

    public override void OnShootUp (Vector3 fireVec) {

    }
}