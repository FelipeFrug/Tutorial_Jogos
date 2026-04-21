using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;

    public AudioSource collectableAudioSource;
    public AudioSource damageAudioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Você vai arrastar os AudioSources pelo Inspector da Unity agora!
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
    
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb.MovePosition(rb.position + movement.normalized * Time.fixedDeltaTime * speed);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coletavel"){
            collectableAudioSource.Play();
            GameController.Collect();
            Destroy(other.gameObject);
        }
        if (other.tag == "Enemy"){
            damageAudioSource.Play();
            GameController.Damage();
            Destroy(other.gameObject);
        }
    }
}
