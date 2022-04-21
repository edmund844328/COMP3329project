using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class abilityControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject mp, hp; //serial this
    [SerializeField] private GameObject cdSpeedUp, cdFlash, cdHeal, cdDefense, cdAttack;
    private float currentMp, currentHp;
    private bool speedUp, healthUp;
    public AudioClip flashSound, healSound, defenseSound, speedSound, attackSound;
    public GameObject flashEffect, healEffect, defenseEffect, attackEffect;
    private Vector3 mousePosition;
    public Camera cam;
    
    void Start()
    {
        // hp = GameObject.Find("Canvas/Elite/Bars/Healthbar");
        // mp = GameObject.Find("Canvas/Elite/Bars/Manabar");
        Debug.Log(mp.GetComponent<Image>().fillAmount);
        currentHp = mp.GetComponent<Image>().fillAmount;
        currentMp = mp.GetComponent<Image>().fillAmount;
        // cdSpeedUp = GameObject.Find("Canvas/speed/cd");
        // cdFlash = GameObject.Find("Canvas/flash/cd");
        // cdHeal = GameObject.Find("Canvas/heal/cd");
        // cdDefense = GameObject.Find("Canvas/defense/cd");
        // cdAttack = GameObject.Find("Canvas/attack/cd");
    }

    // Update is called once per frame
    void Update()
    {
        if (!speedUp)
        {
            if(Input.GetKey(KeyCode.Q) && currentMp > 0.1f && cdSpeedUp.GetComponent<Text>().text == "")
            {
                currentMp -= 0.1f;
                speedUp = true;
                StartCoroutine(Accelerate());
                StartCoroutine(coolDownSpeed());

            }
            mp.GetComponent<Image>().fillAmount = currentMp;
        }

        if (Input.GetKeyDown(KeyCode.E) && currentMp > 0.01f && cdFlash.GetComponent<Text>().text == ""){
            flash();
            StartCoroutine(coolDownFlash());
        }

        if (Input.GetKeyDown(KeyCode.H) && currentMp > 0.01f && cdHeal.GetComponent<Text>().text == ""){
            heal();
            StartCoroutine(coolDownHeal());
        }

        if (Input.GetKeyDown(KeyCode.R) && currentMp > 0.01f && cdDefense.GetComponent<Text>().text == ""){
            StartCoroutine(Defense());
            StartCoroutine(coolDownDefense());
        }

        if (Input.GetKeyDown(KeyCode.T) && currentMp > 0.01f && cdAttack.GetComponent<Text>().text == ""){
            StartCoroutine(Attack());
            StartCoroutine(coolDownAttack());
        }
    }

    IEnumerator Accelerate()
    {
        AudioSource.PlayClipAtPoint(speedSound, this.transform.position);
        immueControl.speed = 5f;
        yield return new WaitForSeconds(3f);
        immueControl.speed = 2f;
        speedUp = false;
    }
    IEnumerator Defense(){
        //reduce the damage taken by a percentage for 10s, reduce 10% per level
        healthBarControl.defense = 1f - healthBarControl.lv*0.1f;
        AudioSource.PlayClipAtPoint(defenseSound, this.transform.position);
        GameObject armor = Instantiate(defenseEffect, this.transform.position + new Vector3(0,0.5f,0), this.transform.rotation);
        Destroy(armor, 0.2f);
        yield return new WaitForSeconds(3f);
        healthBarControl.defense = 1f;
    }
    IEnumerator Attack()
    {
        AudioSource.PlayClipAtPoint(attackSound, this.transform.position);
        GameObject attack = Instantiate(attackEffect, this.transform.position + new Vector3(0,2f,0), this.transform.rotation);
        Destroy(attack, 0.2f);
        healthBarControl.extraDamage = 1f + healthBarControl.lv*0.1f;
        yield return new WaitForSeconds(5f);
        healthBarControl.extraDamage = 1f;
    }
    void flash(){
        mousePosition = Input.mousePosition;
        mousePosition = cam.ScreenToWorldPoint(mousePosition);
        //calculate the mouse position from the character position and normalized it to 1 max
        Vector2 result = (this.transform.position - mousePosition).normalized;
        //the flash effect
        GameObject flash = Instantiate(flashEffect, this.transform.position, Quaternion.identity);
        Destroy(flash,0.1f);
        //times 3 to increase the flash range
        this.transform.Translate(-result*3f);
        AudioSource.PlayClipAtPoint(flashSound, this.transform.position);
    }

    void heal(){
        //healing bases on level, 30(base) + 10 per level
        healthBarControl.currentHP += 30 + healthBarControl.lv*10;
        AudioSource.PlayClipAtPoint(healSound, this.transform.position);
        GameObject healing = Instantiate(healEffect, this.transform.position, this.transform.rotation);
        Destroy(healing, 0.5f);

        if (healthBarControl.currentHP > healthBarControl.maxHP){
            healthBarControl.currentHP = healthBarControl.maxHP;
        }
    }

    IEnumerator coolDownSpeed()
    {
        for (int i = 9; i > 0; i--){
            cdSpeedUp.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        cdSpeedUp.GetComponent<Text>().text = "";
    }
    IEnumerator coolDownFlash()
    {
        for (int i = 9; i > 0; i--){
            cdFlash.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        cdFlash.GetComponent<Text>().text = "";
    }
    IEnumerator coolDownHeal()
    {
        for (int i = 9; i > 0; i--){
            cdHeal.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        cdHeal.GetComponent<Text>().text = "";
    }
    IEnumerator coolDownDefense()
    {
        for (int i = 9; i > 0; i--){
            cdDefense.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        cdDefense.GetComponent<Text>().text = "";
    }
    IEnumerator coolDownAttack()
    {
        for (int i = 9; i > 0; i--){
            cdAttack.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        cdAttack.GetComponent<Text>().text = "";
    }

}

