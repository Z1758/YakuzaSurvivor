using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private int maxHP;
    [SerializeField] private int hp;
    [SerializeField] private int atk;
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float speed;

    public int HP { get { return hp; } set { hp = value; OnHPCHanged?.Invoke(hp); } }
    public int MaxHP { get { return maxHP; } }
    public UnityAction<int> OnHPCHanged;


    public int ATK { get { return atk; } }
    public float DEFAULTSPEED { get { return defaultSpeed; } }
    public float SPEED { get { return speed; } set { speed = value; } }
}
