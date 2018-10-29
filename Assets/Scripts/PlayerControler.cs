using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {

    public GameObject game;
    public GameObject enemyGenerator;

    private Animator animator;

    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        bool gamePlaying = game.GetComponent<GameControler>().gameState == GameState.Playing;
        if (gamePlaying && (Input.GetKeyDown("up") || Input.GetKeyDown(0)))
        {
            UpdateState("PlayerJump");
        }

    }

    public void UpdateState(string state = null) {
        if (state != null) {
            animator.Play(state);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            UpdateState("PlayerDie");
            game.GetComponent<GameControler>().gameState = GameState.Ended;
            enemyGenerator.SendMessage("CancelGenerator", true);
        }
        else if (other.gameObject.tag == "Point")
        {
            game.SendMessage("IncreasePoints");
        }


    }



}
