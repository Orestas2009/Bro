using UnityEngine;



public class ExampleClass : MonoBehaviour
{
    public LayerMask PlayerLayer;
    public float PlayerDistance;
    AudioSource audioSource;
    public GameObject bulletPrefab;
    public float pitchRange = 0.1f;
    public float fireInterval = 0.5f;
    public float fireCooldown;
    public bool Aim;

    private int index = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, PlayerDistance, PlayerLayer);
        if(hit.collider != null)
        {
            if(hit.collider.gameObject.CompareTag("Player") && fireCooldown <= 0)
            {
                print("hit");
                Shoot();
                fireCooldown = fireInterval;
            }
            fireCooldown -= Time.deltaTime;
        }
        
    }
    void Shoot()
    {

        GameObject obj = Instantiate(bulletPrefab, transform.position + transform.right, transform.rotation);
        obj.GetComponent<Bullet>().owner = gameObject;
        audioSource.pitch = Random.Range(1 - pitchRange, 1 + pitchRange);
        audioSource.PlayOneShot(audioSource.clip);
    }
}