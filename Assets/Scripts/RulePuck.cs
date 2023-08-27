
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RulePuck : MonoBehaviour
{
    // Start is called before the first frame update
    private float puckRadius;
    private float hitCount;
    public Image[] hbl,hbr,htl,htr;
    private bool isP1;
    float time;
    private float timeRemaining = 3;
    private GameObject item;
    private Health healtht,healthb;
    List<Action> actions = new List<Action>();
    private float ddtime=0;
    void Start()
    {
        healtht = new Health(450, 135, 190, htl, htr);
        healthb = new Health(450, 135, 190, hbl, hbr);
        ToCenter();
        puckRadius = gameObject.GetComponent<CircleCollider2D>().radius;
        item = GameObject.FindGameObjectWithTag("item");
        hitCount = 0;
        isP1 = false;
        GetActions();
    }
    // Update is called once per frame`
    void Update()
    {
        if (ddtime > 0) {
            ddtime -= Time.deltaTime;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            item.SetActive(true);
            item.transform.position = new Vector2(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-4f, 4f));
            timeRemaining = 3;
        }
        healthb.health = Mathf.Clamp(healthb.health, 0, healthb.maxHealth);
        healtht.health = Mathf.Clamp(healtht.health, 0, healtht.maxHealth);
        hitCount = Mathf.Clamp(hitCount, 0, 50);
        if (healthb.health == 0)
        {
            Debug.Log("Player Top Win");            
            ResetGame();

        }
        else if (healtht.health == 0)
        {
            Debug.Log("Player Bottom Win");
            ResetGame();
        }

        if (gameObject.transform.position.y + puckRadius < -5.34)
        {
            //Ball go to bottom goal line P top score
            ToLoc(new Vector2(0, -1));
            TakeDamage(DamageCalc(hitCount), true);
            ddtime = 0;
            Debug.Log("Top Score");
            Debuging(DamageCalc(hitCount));
            hitCount = 0;
        }
        else if (gameObject.transform.position.y + puckRadius > 5.34)
        {
            //Ball go to top goal line P bottom score
            ToLoc(new Vector2(0, 1));
            TakeDamage(DamageCalc(hitCount), false);
            ddtime = 0;
            Debug.Log("Bottom Score");
            Debuging(DamageCalc(hitCount));
            hitCount = 0;
        }
        HH.HealthBar.UpdateHealthUI(ref healtht, ref time);
        HH.HealthBar.UpdateHealthUI(ref healthb, ref time);
    }
    void ResetGame()
    {
        healthb.health = healthb.maxHealth;
        healtht.health = healtht.maxHealth;
        for(int i=0; i < 3; i++)
        {
            hbl[i].fillAmount = 1;
            hbr[i].fillAmount = 1;
            htl[i].fillAmount = 1;
            htr[i].fillAmount = 1;
        }
        ToCenter();
    }
    void Debuging(float damage)
    {
        Debug.Log("hit = " + hitCount+ " damage "+damage);
    }
    void GetActions()
    {
        actions.Add(() => Ability.RestoreHealth(HealCalc(hitCount), !isP1, ref healthb.health, ref healtht.health, ref time));
        actions.Add(() => Ability.DoubleDamage(ref ddtime));
    }
    void ToCenter()
    {
        Vector2 centerPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));
        ToLoc(centerPos);
    }
    float DamageCalc(float hitCount)
    {
        hitCount /= 10;
        float damage = 200 * (hitCount / (1 + Mathf.Abs(hitCount)));
        if (gameObject.GetComponent<SpriteRenderer>().color == Color.red)
        {
            damage *= 2;
        }
        return damage;
    }
    float HealCalc(float hitCount)
    {
        hitCount /= 10;
        return 100 * (hitCount / (1 + Mathf.Abs(hitCount)));
    }
    void ToLoc(Vector2 pos)
    {
        gameObject.transform.position = pos;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
    }

    void TakeDamage(float damage, bool pb)
    {
        if (pb)
        {
            healthb.health -= damage;
            time = 0f;
        }
        else
        {
            healtht.health -= damage;
            time = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "p1")
        {
            if (isP1 || hitCount==0)
            {
                hitCount++;
                isP1 = false;
            }
        }else if (collision.gameObject.tag == "p2")
        {
            if (!isP1)
            {
                hitCount++;
                isP1 = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "item")
        {
            item.transform.position = new Vector2(Screen.width+10, Screen.height+10);
            item.SetActive(false);
            int i = UnityEngine.Random.Range(0, actions.Count);
            actions[i].Invoke();
        }
    }
}
