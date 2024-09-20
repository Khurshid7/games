using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool isPlayer2; // Check if this is Player 2
    [SerializeField] private GameObject ball;

    private Rigidbody2D rb;
    private Vector2 playerMove;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ApplyPaddleSize();  
    }

    void Update()
    {
        PlayerControl();
    }

    private void PlayerControl()
    {
        if (isPlayer2)
        {
            // Player 2 controls with Up and Down arrow keys
            playerMove = new Vector2(0, Input.GetKey(KeyCode.UpArrow) ? 1 : (Input.GetKey(KeyCode.DownArrow) ? -1 : 0));
        }
        else
        {
            // Player 1 controls with W and S keys
            playerMove = new Vector2(0, Input.GetKey(KeyCode.W) ? 1 : (Input.GetKey(KeyCode.S) ? -1 : 0));
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = playerMove * movementSpeed;
    }

    private void ApplyPaddleSize()
    {
        float paddleSize = PlayerPrefs.GetFloat("PaddleSize", 1f);
        transform.localScale = new Vector3(1, paddleSize, 1);
    }
}
