using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public Status playerStatus;
    public Status enemyStatus;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        playerStatus = new Status(80);
        enemyStatus = new Status(90);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
