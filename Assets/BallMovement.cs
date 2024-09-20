using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 10f;
    [SerializeField] private float speedIncrease = 0.25f;
    [SerializeField] private Text AIScore;
    [SerializeField] private Text PlayerScore;

    private int hitCounter;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartBall", 2f);
    }

    private void FixedUpdate()
    {
        // Retrieve the current ball speed from PlayerPrefs
        float ballSpeed = PlayerPrefs.GetFloat("BallSpeed", initialSpeed); 
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, ballSpeed + (speedIncrease * hitCounter));
    }

    private void StartBall()
    {
        // Retrieve the current ball speed from PlayerPrefs
        float ballSpeed = PlayerPrefs.GetFloat("BallSpeed", initialSpeed);
        rb.velocity = new Vector2(-1, 0) * (ballSpeed + speedIncrease * hitCounter);
    }

    private void ResetBall()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
        hitCounter = 0;
        Invoke("StartBall", 2f);
    }

    private void PlayerBounce(Transform myObject)
    {
        hitCounter++;
        Debug.Log("Hit Counter: " + hitCounter);

        Vector2 ballPos = transform.position;
        Vector2 playerPos = myObject.position;

        float xDirection = (transform.position.x > 0) ? -1 : 1;
        float yDirection = (ballPos.y - playerPos.y) / myObject.GetComponent<Collider2D>().bounds.size.y;
        
        // Ensure yDirection has a minimum value to prevent flat bounces
        if (yDirection == 0)
        {
            yDirection = 0.25f;
        }

        float ballSpeed = PlayerPrefs.GetFloat("BallSpeed", initialSpeed);
        rb.velocity = new Vector2(xDirection, yDirection) * (ballSpeed + (speedIncrease * hitCounter));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with: " + collision.gameObject.name);
        if (collision.gameObject.name == "Player" || collision.gameObject.name == "AI")
        {
            PlayerBounce(collision.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > 0)
        {
            ResetBall();
            int playerScore = int.Parse(PlayerScore.text) + 1;
            PlayerScore.text = playerScore.ToString();
        }
        else if (transform.position.x < 0)
        {
            ResetBall();
            int aiScore = int.Parse(AIScore.text) + 1;
            AIScore.text = aiScore.ToString();
        }
    }
}
