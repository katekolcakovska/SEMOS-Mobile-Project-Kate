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
    [SerializeField] private GameObject startScreen;

    public UnityEvent OnHit;
    public UnityEvent OnPoint;
    public UnityEvent OnJump;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {

        Time.timeScale = 0;
        startScreen.SetActive(true);
        rb = GetComponent<Rigidbody2D>();

        // unpause game
        
    }

    // Update is called once per frame
    void Update()
    {

        if (GetJumpInput())
        {
            Debug.Log("Player pressed the jump button");
            // start game
            Time.timeScale = 1;
            //remove startScreen
            startScreen.SetActive(false);
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

 /*   public void StartGame()
    {
        startScreen.SetActive(false);
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        
    }*/
}