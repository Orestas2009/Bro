using UnityEngine;



public class ExampleClass : MonoBehaviour
{
    public LayerMask PlayerLayer;
    public float PlayerDistance;
    AudioSource audioSource;
    public GameObject bulletPrefab;
    public float pitchRange = 0.1f;
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, PlayerDistance, PlayerLayer);
        
        if (hit.collider.gameObject.CompareTag("Player"))
        {
            Shoot();
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