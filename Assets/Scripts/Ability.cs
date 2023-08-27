
using UnityEngine;

public class Ability
{
    public static void RestoreHealth(float value, bool pb, ref float healthb,ref float healtht,ref float time)
    {
        if (pb)
        {
            Debug.Log("Heal Bottom "+value);
            healthb += value;
            time = 0f;
        }
        else
        {
            Debug.Log("Heal Top " + value);
            healtht += value;
            time = 0f;
        }
    }
    public static void DoubleDamage(ref float ddTime)
    {
        Debug.Log("Double Damage");
        GameObject.FindGameObjectWithTag("ball").GetComponent<SpriteRenderer>().color = Color.red;
        ddTime = 5f;
    }

}
