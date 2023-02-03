using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogicManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private List<GameObject> startingPositions;
    public List<GameObject> players;
    public float totalScore;
    public Text scoreText;
    public Text gameOverText;
    private bool isGameOver = false;

    void generatePlayers(int playerCount) {
        int partialDegrees = 360 / playerCount;
        for (int i = 0; i < playerCount; i++) {
            Debug.Log("Generating player " + i);
            GameObject player = Instantiate(playerPrefab);
            player.GetComponentInChildren<HeadScript>().transform.position = startingPositions[i].transform.position;
            player.GetComponentInChildren<HeadScript>().movementControls = (MovementControls)i;
            player.GetComponentInChildren<HeadScript>().transform.rotation = Quaternion.Euler(0, 0, 90 + partialDegrees * i);
            players.Add(player);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        generatePlayers(2);
    }

    // Update is called once per frame
    void Update()
    {
        // update total score
        if (isGameOver)
        {
            return;
        }
        totalScore = 0;
        bool isGameoverInner = true;
        foreach (GameObject player in players)
        {
            var player_script = player.GetComponent<PlayerScript>();
            totalScore += player_script.score;
            if (player_script.isAlive)
            {
                isGameoverInner = false;
            }
        }
        scoreText.text = "Score: " + (int)System.Math.Round(totalScore);
        if (isGameoverInner)
        {
            GameOver();
        }
    }
    private void GameOver() {
        Debug.Log("Game Over");
        isGameOver = true;
        gameOverText.enabled = true;
    }
}
