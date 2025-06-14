using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 dir;
    private int liveScore = 1;
    public GameObject gameOver;
    public float restartDelay = 3f;
    [SerializeField] private int speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;

    //private int lineToMove = 1;
   // public float lineDistance = 4;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        /*
         if (SwipeController.swipeRight)
         {
             if (lineToMove < 2)
                 lineToMove++;
         }

         if (SwipeController.swipeLeft)
         {
             if (lineToMove > 0)
                 lineToMove--;
         }
        */
        if (Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.Space))
        {
            // if (controller.isGrounded)
            Debug.Log("Свайп сработал");
                Jump();
        }
        //if (liveScore==0)
        //{
        //    gameObject.SetActive(true);
        //    Time.timeScale = 0f;

        //    // Запустить перезапуск с задержкой
        //    Invoke(nameof(RestartGame), restartDelay);
        //}
       /* Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove == 0)
            targetPosition += Vector3.left * lineDistance;
        else if (lineToMove == 2)
            targetPosition += Vector3.right * lineDistance;

        transform.position = targetPosition;*/
    }

    private void Jump()
    {
        dir.y = jumpForce;
    }

    void FixedUpdate()
    {
        dir.z = speed;
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Сvthnm");
            //liveScore--;
            gameOver.SetActive(true);
            Debug.Log("Сvthnm1");
            Time.timeScale = 0f;

            // Запустить перезапуск с задержкой
            //Invoke(nameof(RestartGame), restartDelay);
        }
    }
    //private void RestartGame()
    //{
    //    Time.timeScale = 1f; // Восстановить время
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}
}