using UnityEngine;

public class SimpleWeapon : BaseWeapon
{
    public override string Name => "Simple";

    public override string IconPath => "Icons/S_Forward_ray";

    protected override void Awake()
    {
        base.Awake();
        bulletPrefab = GameRepository.Instance.simpleWeaponPrefab;
        bullets = -1;
    }

    public override void OnShootDown(Vector3 fireVec, Transform playerTransform, bool isTouch = false)
    {
        Shoot(fireVec, playerTransform);
    }

    public override void OnShootUp(Vector3 fireVec, Transform playerTransform, bool isTouch = false)
    {

    }
}