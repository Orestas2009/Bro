using UnityEngine;

public class ExampleClass : MonoBehaviour
{
    public LayerMask PlayerLayer;
    public LayerMask GroundLayer;
    public float groundDistance = 1f;
    public float PlayerDistance = 10f;
    public float moveSpeed = 2f;
    public GameObject bulletPrefab;
    public float pitchRange = 0.1f;
    public float fireInterval = 0.5f;
    public bool Aim;
    public bool canMove = true;

    private AudioSource audioSource;
    private float fireCooldown;
    private Vector3 moveDirection = Vector3.right;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (canMove)
        {
            MoveAndTurn();
        }

        DetectPlayer();
    }

    void MoveAndTurn()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        Vector2 groundDirection = new Vector2(moveDirection.x, -1).normalized;
        RaycastHit2D groundCheck = Physics2D.Raycast(transform.position, groundDirection, groundDistance, GroundLayer);
        if (groundCheck.collider == null)
        {
            TurnAround();
        }

        RaycastHit2D obstacleCheck = Physics2D.Raycast(transform.position, moveDirection, groundDistance, GroundLayer);
        if (obstacleCheck.collider != null)
        {
            TurnAround();
        }
    }

    void DetectPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, PlayerDistance, PlayerLayer);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Player") && fireCooldown <= 0)
        {
            Shoot();
            fireCooldown = fireInterval;
        }
        fireCooldown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject obj = Instantiate(bulletPrefab, transform.position + moveDirection, transform.rotation);
        obj.GetComponent<Bullet>().owner = gameObject;
        if (audioSource != null)
        {
            audioSource.pitch = Random.Range(1 - pitchRange, 1 + pitchRange);
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

    void TurnAround()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        moveDirection *= -1;
    }
}
