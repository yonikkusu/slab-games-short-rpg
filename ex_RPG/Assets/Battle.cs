using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum BattleState
{
    START,
    PLAYER_TURN,
    ENEMY_TURN,
}

public class Battle : MonoBehaviour
{
    [SerializeField] AttackButton attackButton;
    private StatusManager playerStatus;
    private StatusManager enemyStatus;
    BattleState state = BattleState.START;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("battle scene");

        playerStatus = GameObject.Find("StatusManager").GetComponent<StatusManager>();
        Debug.Log("player HP: " + playerStatus.playerStatus.hp);

        enemyStatus = GameObject.Find("StatusManager").GetComponent<StatusManager>();
        Debug.Log("enemy HP: " + enemyStatus.enemyStatus.hp);

        Debug.Log(attackButton.Click);
    }

    // Update is called once per frame
    void Update()
    {
        switch( state )
        {
            case BattleState.START:
                Debug.Log("current state: " + state.ToString());
                state = BattleState.PLAYER_TURN;
                break;

            case BattleState.PLAYER_TURN:
                //Debug.Log("current state: " + state.ToString());
                //Debug.Log("button click: " + attackButton.click);

                if( attackButton.Click )
                {
                    Debug.Log("current state: " + state);

                    // player attack
                    enemyStatus.enemyStatus.ChangeHp(-Random.Range(30, 40));
                    attackButton.Click = false;
                    state = BattleState.ENEMY_TURN;
                }
                
                break;

            case BattleState.ENEMY_TURN:
                Debug.Log("current state: " + state.ToString());

                // enemy attack
                playerStatus.playerStatus.ChangeHp(-Random.Range(30, 40));
                state = BattleState.PLAYER_TURN;
                break;

            default:
                break;
        }

    }
}
