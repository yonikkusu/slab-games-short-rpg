using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status
{
    private int maxHp;
    public int hp { get; set; }

    public Status(int maxHp)
    {
        this.maxHp = maxHp;
        this.hp = maxHp;
        
    }

    public bool ChangeHp(int value )
    {
        this.hp += value;
        
        if( this.hp > this.maxHp )
        {
            this.hp = this.maxHp;
        }
        else if( this.hp < 0 )
        {
            this.hp = 0;
            return false;
        }

        return true;
    }
}
