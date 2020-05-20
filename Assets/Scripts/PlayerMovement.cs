using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent agent;
    Transform agentTransform;
    
    public Text scoreText;
    public Text moveText;
    public Text gameOver;

    int score;
    int moves;

    bool gameDone = false;

    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        score = 0;
        moves = 20;
        gameOver.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && moves > 0)
        {
            //Variable to hold mouse position
            Vector3 mousePos = Input.mousePosition;

            //Use the current camera to convert mouse position to a ray
            Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);

            //Create a plane that faces up at the same position as the player
            Plane playerPosPlane = new Plane(Vector3.up, transform.position);

            //How far along the ray does the intersection with the plan occur?
            float rayDistance = 0;
            playerPosPlane.Raycast(mouseRay, out rayDistance);

            //Use the ray distance to calculate the point of collision
            Vector3 targetPoint = mouseRay.GetPoint(rayDistance);

            //Move agent to target point
            agent.SetDestination(targetPoint);
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, 0);

            SetMoveText();
            return;
        }
        else if(moves <= 0)
        {
            gameOver.text = "Game Over!\nClick to Continue";
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            score += 1;
            SetScoreText();
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();        
    }

    void SetMoveText()
    {
        moves--;
        moveText.text = "Moves Left: " + moves.ToString();
    }
}