using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
 
[RequireComponent(typeof(Rigidbody2D))]
public class BirdController : MonoBehaviour
{
    [SerializeField] private float jumpSpeed;
    [SerializeField] private GameObject gameOverScreen;

    public UnityEvent OnHit;
    public UnityEvent OnPoint;
    public UnityEvent OnJump;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // unpause game
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

        if (GetJumpInput())
        {
            Debug.Log("Player pressed the jump button");
            Jump();
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        // pause game
        Time.timeScale = 0;

        // show game over screen
        if (gameOverScreen != null)
            gameOverScreen.SetActive(true);


        // show points

    }

    private bool GetJumpInput()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Jump()
    {
        // calculate the direction and speed
        Vector2 jumpDirection = new Vector2(0, 1);
        Vector2 jumpVector = jumpDirection * jumpSpeed;

        // reset the speed
        rb.velocity = Vector2.zero;

        // jump
        rb.AddForce(jumpVector, ForceMode2D.Impulse);

        //trigger event
        OnJump?.Invoke();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}