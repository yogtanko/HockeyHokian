using UnityEngine;
using UnityEngine.UI;

namespace HH
{    
    class HealthBar
    {
        public static void UpdateHealthUI(ref Health health,ref float time)
        {
            time += Time.deltaTime;
            float lerptime = time / health.lerpspeed;
            if ((health.health > health.h2 + health.h3 || health.hl[0].fillAmount > 0) && health.hl[1].fillAmount == 1)
            {
                health.hl[1].fillAmount = 1;
                health.hr[1].fillAmount = 1;
                health.hl[0].fillAmount = Mathf.Lerp(health.hl[0].fillAmount, (health.health - health.h2 - health.h3) / health.h1, lerptime);
                health.hr[0].fillAmount = Mathf.Lerp(health.hr[0].fillAmount, (health.health - health.h2 - health.h3) / health.h1, lerptime);
            }
            else if (health.health > health.h3 || health.hl[1].fillAmount > 0 && health.hl[2].fillAmount == 1)
            {
                health.hl[2].fillAmount = 1;
                health.hr[2].fillAmount = 1;
                health.hl[1].fillAmount = Mathf.Lerp(health.hl[1].fillAmount, (health.health - health.h3) / health.h2, lerptime);
                health.hr[1].fillAmount = Mathf.Lerp(health.hr[1].fillAmount, (health.health - health.h3) / health.h2, lerptime);
            }
            else if (health.health > 0 || health.hl[2].fillAmount > 0)
            {
                health.hl[2].fillAmount = Mathf.Lerp(health.hl[2].fillAmount, health.health / health.h3, lerptime);
                health.hr[2].fillAmount = Mathf.Lerp(health.hr[2].fillAmount, health.health / health.h3, lerptime);
            }
            else
            {
                health.hl[2].fillAmount = Mathf.Lerp(health.hl[2].fillAmount, 0, lerptime);
                health.hr[2].fillAmount = Mathf.Lerp(health.hr[2].fillAmount, 0, lerptime);
            }

        }
    }
}

