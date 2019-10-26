using UnityEngine;

public class SimpleWeapon : BaseWeapon {
    public override string Name => "Simple";

    public override string IconPath => "Icons/Tazo_2D/Icon/S_Forward_ray";

    private void Awake () {
        bulletPrefab = Resources.Load<GameObject> ("Prefabs/SimpleWeaponBullet");
        bullets = -1;
    }

    public override void OnShootDown (Vector3 fireVec, Transform playerTransform) {
        Shoot (fireVec, playerTransform);
    }

    public override void OnShootUp (Vector3 fireVec, Transform playerTransform) {

    }
}