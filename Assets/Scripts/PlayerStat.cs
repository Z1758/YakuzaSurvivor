using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum BattleStyleType
{
    Thug, Slugger, Breaker, Legend, End
}



public class PlayerStat : MonoBehaviour
{

    private static PlayerStat instance;

    [SerializeField] private int maxHP;
    [SerializeField] private int hp;
    [SerializeField] private int maxExp;
    [SerializeField] private int exp;
    [SerializeField] private float maxHitGauge;
    [SerializeField] private float hitGauge;
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

    public int EXP { get { return exp; } set { exp = value; OnEXPChanged?.Invoke(exp); } }
    public int MaxEXP { get { return maxExp; } }
    public UnityAction<int> OnEXPChanged;

    public float HitGauge { get { return hitGauge; } set { hitGauge = value; OnHitGaugeChanged?.Invoke(hitGauge); } }
    public float MaxHitGauge { get { return maxHitGauge; } }
    public UnityAction<float> OnHitGaugeChanged;


    public int StyleIndex { get { return styleIndex; } set { styleIndex = value; OnStyleChanged?.Invoke(styleIndex); } }
    public UnityAction<int> OnStyleChanged;

    public int ATK { get { return atk; } set{ atk = value; } }
    public float DEFAULTSPEED { get { return defaultSpeed; } set { defaultSpeed = value; OnDefaultSpeedChanged?.Invoke(); } }
    public UnityAction OnDefaultSpeedChanged;
    public float SPEED { get { return speed; } set { speed = value; } }
   
    public AttackMultiplier[] Multiplier { get { return multiplier; } }
}
