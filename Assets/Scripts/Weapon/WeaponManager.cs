using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private bool semiAuto;
    private float _fireRateTimer;

    [Header("Bullet Properties")] 
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform barrelPos;
    [SerializeField] private float bulletVelocity = 10f;
    [SerializeField] private int bulletsPerShot;
    private AimStateManager _aim;

    [SerializeField] private AudioClip gunShot;
    private AudioSource _audioSource;
    
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _aim = GetComponentInParent<AimStateManager>();
        _fireRateTimer = fireRate;
    }

    
    void Update()
    {
        if (ShouldFire()) Fire();
    }

    bool ShouldFire()
    {
        _fireRateTimer += Time.deltaTime;
        if (_fireRateTimer < fireRate) return false;
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;
        return false;
    }

    void Fire()
    {
        _fireRateTimer = 0;
        barrelPos.LookAt(_aim.aimPos);
        _audioSource.PlayOneShot(gunShot);
        for (int i = 0; i < bulletsPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }
    }
}
