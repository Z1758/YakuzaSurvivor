using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatController : MonoBehaviour
{
    [SerializeField] PlayerStat model;
    [SerializeField] Slider hpSlider;
    [SerializeField] Image styleImageUI;
    [SerializeField] Sprite[] styleImages;
    // Start is called before the first frame update

    private void OnEnable()
    {
        model.OnHPChanged += UpdateHP;
        model.OnStyleChanged += UpdateStyle;
  
    }
    private void OnDisable()
    {
        model.OnHPChanged -= UpdateHP;
        model.OnStyleChanged -= UpdateStyle;
    }

    private void Start()
    {
        hpSlider.maxValue = model.MaxHP;
     
        UpdateHP(model.HP);
        UpdateStyle(model.StyleIndex);

    }


    private void UpdateHP(int hp)
    {

        hpSlider.value = hp;

    }

    private void UpdateStyle(int num)
    {

        styleImageUI.sprite = styleImages[num];

    }

}
