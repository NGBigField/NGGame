using UnityEngine;

public class PlasmaChargeWeapon : BaseWeapon {
    private float chargeStartTime;

    public override string Name => "Plasma";

    public override void OnShootDown (Vector3 fireVec, Transform playerTransform) {
        chargeStartTime = Time.time;
    }

    public override void OnShootUp (Vector3 fireVec, Transform playerTransform) {
        var deltaTime = Time.time - chargeStartTime;
        Shoot (fireVec, playerTransform);
    }
}