using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbs : MonoBehaviour
{
    public int speed;
    public int HP;
    public int Blood;

    public abstract void Attack();

    public virtual void Die(){
        Destroy(this.gameObject);
    }

}
