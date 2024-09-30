using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
   public enum PotionType
    {
        HP,HitGauge, END
    }

    [SerializeField] PotionType potionType;
    [SerializeField] int amount;
    public void UseItem()
    {
        switch (potionType)
        {
            case PotionType.HP:
                if (PlayerStat.Instance.MaxHP < PlayerStat.Instance.HP + amount)
                {
                    PlayerStat.Instance.HP = PlayerStat.Instance.MaxHP;
                }
                else
                {

                    PlayerStat.Instance.HP += amount;
                }
                break;
            case PotionType.HitGauge:
                if (PlayerStat.Instance.MaxHitGauge < PlayerStat.Instance.HitGauge + amount)
                {
                    PlayerStat.Instance.HitGauge = PlayerStat.Instance.MaxHitGauge;
                }
                else
                {

                    PlayerStat.Instance.HitGauge += amount;
                }
                break;
        }

        Destroy(gameObject);
    } 
}
