using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHP : MonoBehaviour
{
    private GameObject statusObject;
    private StatusManager status; 
    private Text textHp;
    public int selTarget; // 0: player, 1: enemy

    // Start is called before the first frame update
    void Start()
    {
        statusObject = GameObject.Find("StatusManager");
        status = statusObject.GetComponent<StatusManager>();

        textHp = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        switch( selTarget )
        {
            case 0:
                textHp.text = status.playerStatus.hp.ToString();
                break;

            case 1:
                textHp.text = status.enemyStatus.hp.ToString();
                break;

            default :
                break;

        }
    }
}
