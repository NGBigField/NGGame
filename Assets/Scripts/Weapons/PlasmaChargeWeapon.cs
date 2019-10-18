using UnityEngine;

public class PlasmaChargeWeapon : BaseWeapon {
    private float chargeStartTime;
    public override void OnShootDown (Vector3 fireVec) {
        chargeStartTime = Time.time;
    }

    public override void OnShootUp (Vector3 fireVec) {
        var deltaTime = Time.time - chargeStartTime;
        Shoot (fireVec);
    }
}