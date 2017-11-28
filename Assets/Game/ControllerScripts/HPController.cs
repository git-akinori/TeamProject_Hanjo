using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPController : MonoBehaviour{

    [SerializeField]
    private const int maxHp = 500;
    private static int hp;

    [SerializeField]
    bool log = false;

    private void Start()
    {
        hp = maxHp;
    }

    // _damage で hp を減算
    public void DamageCalc(int _damage)
    {
        hp -= _damage;
        if (hp < 0) hp = 0;
        if(log) Debug.Log(hp);
    }

    // HPの割合を返す
    public float HpRatio
    {
        get { return ((float)hp) / maxHp; }
    }
}
