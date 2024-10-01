using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatController : MonoBehaviour
{
    [SerializeField] PlayController controller;
    [SerializeField] PlayerStat model;
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider expSlider;
    [SerializeField] Slider hitgaugeSlider;
    [SerializeField] Image styleImageUI;
    [SerializeField] Sprite[] styleImages;

    Coroutine hgCoroutine;

    private void OnEnable()
    {
        model.OnHPChanged += UpdateHP;
        model.OnStyleChanged += UpdateStyle;
        model.OnEXPChanged += UpdateEXP;
        model.OnMaxEXPChanged += UpdateMaxEXP;
        model.OnHitGaugeChanged += UpdateHitGauge;

    }
    private void OnDisable()
    {
        model.OnHPChanged -= UpdateHP;
        model.OnStyleChanged -= UpdateStyle;
        model.OnEXPChanged -= UpdateEXP;
        model.OnMaxEXPChanged -= UpdateMaxEXP;
        model.OnHitGaugeChanged -= UpdateHitGauge;
    }

    private void Start()
    {
        controller = GetComponent<PlayController>();
        model = GetComponent<PlayerStat>();

        hpSlider.maxValue = model.MaxHP;
        expSlider.maxValue = model.MaxEXP;
        hitgaugeSlider.maxValue = 100;


        UpdateHP(model.HP);
        UpdateEXP(model.EXP);
        UpdateMaxEXP(model.MaxEXP);
        UpdateHitGauge(model.HitGauge);
        UpdateStyle(model.StyleIndex);

    }


    private void UpdateHP(int hp)
    {

        hpSlider.value = hp;

    }

    private void UpdateEXP(int exp)
    {

        expSlider.value = exp;


    }
    private void UpdateMaxEXP(int maxExp)
    {

        expSlider.maxValue = maxExp;


    }
    private void UpdateHitGauge(float gauge)
    {

        hitgaugeSlider.value = gauge;

    }

    private void UpdateStyle(int num)
    {

        styleImageUI.sprite = styleImages[num];
        if (model.StyleIndex == (int)BattleStyleType.Legend)
        {
            hgCoroutine = StartCoroutine(DecreaseHitGauge());
        }
        else
        {
            StopAllCoroutines();
        }

    }

    IEnumerator DecreaseHitGauge()
    {
        while (0 < model.HitGauge)
        {
            yield return null;
            model.HitGauge -= Time.deltaTime * 5f;
            if (model.HitGauge < 0)
            {
                model.HitGauge = 0;
            }
        }



        controller.ChangeStyle((int)BattleStyleType.Thug);

    }

}
