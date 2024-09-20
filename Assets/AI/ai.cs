using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI_Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool isAI;  // Flag to determine if this is an AI-controlled paddle
    [SerializeField] private GameObject ball; // Reference to the ball
    private float aiSpeedModifier = 1f;  // Modifier for AI speed based on difficulty

    private Rigidbody2D rb;
    private Vector2 playerMove;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (isAI)
        {
            ApplyAIDifficulty(); // Apply the AI difficulty settings
        }
    }

    void Update()
    {
        if (isAI)
        {
            AIControl();
        }
        else
        {
            PlayerControl();
        }
    }

    private void PlayerControl()
    {
        // For player, we use input for vertical movement
        playerMove = new Vector2(0, Input.GetAxisRaw("Vertical"));
    }

    private void AIControl()
    {
        // AI follows the ball’s Y position, modified by AI speed
        if (ball.transform.position.y > transform.position.y + 0.5f)
        {
            playerMove = new Vector2(0, 1); // Move AI paddle up
        }
        else if (ball.transform.position.y < transform.position.y - 0.5f)
        {
            playerMove = new Vector2(0, -1); // Move AI paddle down
        }
        else
        {
            playerMove = new Vector2(0, 0); // Stay still
        }
    }

    private void FixedUpdate()
    {
        // Apply the movement to the rigidbody with the AI speed modifier
        rb.velocity = playerMove * movementSpeed * aiSpeedModifier;
    }

    private void ApplyAIDifficulty()
    {
        // Get the AI difficulty level from PlayerPrefs (default is Medium)
        int difficulty = PlayerPrefs.GetInt("AIDifficulty", 1);

        // Adjust the AI speed modifier based on difficulty
        switch (difficulty)
        {
            case 0:
                aiSpeedModifier = 0.5f; // Easy difficulty, slower AI
                break;
            case 1:
                aiSpeedModifier = 1f; // Medium difficulty, normal speed
                break;
            case 2:
                aiSpeedModifier = 1.5f; // Hard difficulty, faster AI
                break;
            default:
                aiSpeedModifier = 1f; // Default to Medium if difficulty is out of range
                break;
        }
    }
}
