using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBarControl : MonoBehaviour
{
    private GameObject cv;//canvas
    private GameObject et;//elite
    private GameObject nm;//name
    private GameObject hpBar;
    private GameObject mpBar;
    private GameObject lvCount;
    public static float maxHP;
    static float maxMP;
    public static float currentHP;
    public static float currentMP;
    public static int lv;
    // Start is called before the first frame update
    void Start()
    {
        maxHP = maxMP = currentHP = currentMP = 100;
        lv = 1;
        cv = GameObject.Find("Canvas");
        et = GameObject.Find("Canvas/Elite");
        nm = GameObject.Find("Canvas/Elite/Name");
        lvCount = GameObject.Find("Canvas/Elite/Level/Text");
        hpBar = GameObject.Find("Canvas/Elite/Bars/Healthbar");
        mpBar = GameObject.Find("Canvas/Elite/Bars/Manabar");
        nm.GetComponent<Text>().text = "Immue";
        updateBar();
    }

    // Update is called once per frame
    void Update()
    {
        updateBar();
    }

    void updateBar(){
        hpBar.GetComponent<Image>().fillAmount = currentHP/maxHP;
        mpBar.GetComponent<Image>().fillAmount = currentMP/maxMP;
        lvCount.GetComponent<Text>().text = lv.ToString();
    }
}