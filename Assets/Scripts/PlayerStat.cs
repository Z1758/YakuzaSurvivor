using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStat : MonoBehaviour
{

    private static PlayerStat instance;

    [SerializeField] private int maxHP;
    [SerializeField] private int hp;
    [SerializeField] private int atk;
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float speed;
    [SerializeField] private int styleIndex;
    [SerializeField] private AttackMultiplier[] multiplier;


    private void Awake()
    {
        if (null == instance)
        {

            instance = this;

        }
    }

    public static PlayerStat Instance
    {

        get
        {

            return instance;
        }

    }
    public int HP { get { return hp; } set { hp = value; OnHPChanged?.Invoke(hp); } }
    public int MaxHP { get { return maxHP; } }
    public UnityAction<int> OnHPChanged;

    public int StyleIndex { get { return styleIndex; } set { styleIndex = value; OnStyleChanged?.Invoke(styleIndex); } }
    public UnityAction<int> OnStyleChanged;

    public int ATK { get { return atk; } set{ atk = value; } }
    public float DEFAULTSPEED { get { return defaultSpeed; } set { defaultSpeed = value; OnDefaultSpeedChanged?.Invoke(); } }
    public UnityAction OnDefaultSpeedChanged;
    public float SPEED { get { return speed; } set { speed = value; } }
   
    public AttackMultiplier[] Multiplier { get { return multiplier; } }
}
