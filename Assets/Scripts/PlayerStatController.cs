using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatController : MonoBehaviour
{
    [SerializeField] PlayerStat model;
    [SerializeField] Slider hpSlider;
    // Start is called before the first frame update

    private void OnEnable()
    {
        model.OnHPCHanged += UpdateHP;
  
    }
    private void OnDisable()
    {
        model.OnHPCHanged -= UpdateHP;
      
    }

    private void Start()
    {
        hpSlider.maxValue = model.MaxHP;
     
        UpdateHP(model.HP);
    

    }


    private void UpdateHP(int hp)
    {

        hpSlider.value = hp;

    }

}
