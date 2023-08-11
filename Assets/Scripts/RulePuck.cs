
using JetBrains.Annotations;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class RulePuck : MonoBehaviour
{
    // Start is called before the first frame update
    private float puckRadius;
    private float hitCount;
    public Image[] hbl,hbr,htl,htr;
    private float healthb,healtht, maxHealth;
    private float h1 = 450, h2 = 135, h3 = 190;
    private bool isP1;
    float lerpspeed = 2f, time;
    private float timeRemaining = 3;
    private GameObject item;
    void Start()
    {
        ToCenter();
        puckRadius = gameObject.GetComponent<CircleCollider2D>().radius;
        item = GameObject.FindGameObjectWithTag("item");
        maxHealth = h1+h2+h3;
        healthb = maxHealth;
        healtht = maxHealth;
        hitCount = 0;
        isP1 = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            item.SetActive(true);
            item.transform.position = new Vector2(Random.Range(-2f, 2f), Random.Range(-4f, 4f));
            timeRemaining = 3;
        }
        healthb = Mathf.Clamp(healthb, 0, maxHealth);
        healtht = Mathf.Clamp(healtht, 0, maxHealth);
        hitCount = Mathf.Clamp(hitCount, 0, 50);
        if (healthb == 0)
        {
            Debug.Log("Player Top Win");            
            ResetGame();

        }
        else if (healtht == 0)
        {
            Debug.Log("Player Bottom Win");
            ResetGame();
        }

        if (gameObject.transform.position.y + puckRadius < -5.34)
        {
            //Ball go to bottom goal line P top score
            ToLoc(new Vector2(0, -1));
            TakeDamage(DamageCalc(hitCount), true);
            //Debuging(DamageCalc(hitCount));
            hitCount = 0;
        }
        else if (gameObject.transform.position.y + puckRadius > 5.34)
        {
            //Ball go to top goal line P bottom score
            ToLoc(new Vector2(0, 1));
            TakeDamage(DamageCalc(hitCount), false);
            //Debuging(DamageCalc(hitCount));
            hitCount = 0;
        }
        UpdateHealthUI(htl,htr,healtht);
        UpdateHealthUI(hbl, hbr, healthb);
    }
    void ResetGame()
    {
        healthb = maxHealth;
        healtht = maxHealth;
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
        Debug.Log("hit = " + hitCount);
        Debug.Log("damage = " + damage);

    }
    void ToCenter()
    {
        Vector2 centerPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));
        ToLoc(centerPos);
    }
    float DamageCalc(float hitCount)
    {
        hitCount /= 10;
        return 200 *(hitCount/(1+Mathf.Abs(hitCount)));
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

    void UpdateHealthUI(Image[] left, Image[] right, float health)
    {
        time += Time.deltaTime;
        float lerptime = time / lerpspeed;
        if((health > h2 + h3 || left[0].fillAmount>0) && left[1].fillAmount==1)
        {
            left[1].fillAmount = 1;
            right[1].fillAmount = 1;
            left[0].fillAmount = Mathf.Lerp(left[0].fillAmount, (health - h2 - h3) / h1, lerptime);
            right[0].fillAmount = Mathf.Lerp(right[0].fillAmount, (health - h2 - h3) / h1, lerptime);
        }else if (health > h3 || left[1].fillAmount > 0 && left[2].fillAmount == 1)
        {
            left[2].fillAmount = 1;
            right[2].fillAmount = 1;
            left[1].fillAmount = Mathf.Lerp(left[1].fillAmount, (health - h3) / h2, lerptime);
            right[1].fillAmount = Mathf.Lerp(right[1].fillAmount, (health - h3) / h2, lerptime);
        }
        else if(health > 0 || left[2].fillAmount > 0) 
        {
            left[2].fillAmount = Mathf.Lerp(left[2].fillAmount, health / h3, lerptime);
            right[2].fillAmount = Mathf.Lerp(right[2].fillAmount, health / h3, lerptime);
        }
        else
        {
            left[2].fillAmount = Mathf.Lerp(left[2].fillAmount, 0, lerptime);
            right[2].fillAmount = Mathf.Lerp(right[2].fillAmount, 0, lerptime);
        }

    }
    void TakeDamage(float damage, bool pb)
    {
        if (pb)
        {
            healthb -= damage;
            time = 0f;
        }
        else
        {
            healtht -= damage;
            time = 0f;
        }
    }
    void RestoreHealth(float value,bool pb)
    {
        if (pb)
        {
            healthb += value;
            time = 0f;
        }
        else
        {
            healtht += value;
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
            RestoreHealth(HealCalc(hitCount),!isP1);
        }
    }
}
