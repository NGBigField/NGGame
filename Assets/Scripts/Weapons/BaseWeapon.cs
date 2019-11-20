using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour {
    public static readonly int INFINITE_BULLETS = -1;
    public abstract string Name { get; }
    public abstract string IconPath { get; }

    public AudioClip emptyClipSound;

    protected AudioSource audioSource;

    protected PlayerControl playerControl;

    protected virtual void Awake () {
        emptyClipSound = GameRepository.Instance.emptyClipSound;
        audioSource = GetComponent<AudioSource> ();
        playerControl = GetComponentInParent<PlayerControl> ();
    }

    private Texture icon;
    public Texture Icon {
        get {
            if (!icon && IconPath != null)
                icon = Resources.Load<Texture> (IconPath);

            return icon;
        }
    }

    protected int bullets;

    public virtual int Bullets {
        get { return bullets; }
        private set { bullets = value; }
    }

    public virtual bool CanShoot {
        get {
            return bullets == INFINITE_BULLETS || bullets > 0;
        }
    }

    public virtual bool IsMagazineEmpty {
        get {
            return bullets == 0;
        }
    }

    /// <summary>
    /// The power of the bullet shot from the weapon.
    /// </summary>
    public float kickVelocityFactor = 2f;

    public GameObject bulletPrefab;

    /// <summary>
    /// Shoots the bullet by instantiating it's bullet prefab.
    /// </summary>
    /// <param name="fireVec"></param>
    protected virtual void Shoot (Vector3 fireVec, Transform playerTransform, bool isTouch = false) {
        if (!CanShoot) {
            audioSource.PlayOneShot (emptyClipSound);
            return;
        }

        /* Create Bullet  */
        var bullet = Instantiate (bulletPrefab, playerTransform.position + Vector3.up * 0.8f, Quaternion.identity);
        var bulletLogic = bullet.GetComponent<BulletLogic> ();

        bulletLogic.LaunchBullet (fireVec * kickVelocityFactor);
        if (Bullets != INFINITE_BULLETS) --bullets;

    }

    /// <summary>
    /// Called when shoot button is held down. On mobile touch interactions, this will be called everytime the user clicks on the screen without dragging.
    /// </summary>
    /// <param name="fireVec"></param>
    public abstract void OnShootDown (Vector3 fireVec, Transform playerTransform, bool isTouch = false);

    /// <summary>
    /// Called when shoot button is released. On mobile touch interactions, this will not be called at all, please use OnShootDown to detect is a previous shoot was called to create logic like charging an attack.
    /// </summary>
    /// <param name="fireVec"></param>
    public abstract void OnShootUp (Vector3 fireVec, Transform playerTransform, bool isTouch = false);
}