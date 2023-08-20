using UnityEngine.UI;

struct Health
{
    public float h1, h2, h3,lerpspeed;
    public float health, maxHealth;
    public Image[] hl, hr;

    public Health(float h1, float h2, float h3, Image[] hl, Image[] hr)
    {
        this.h1 = h1;
        this.h2 = h2;
        this.h3 = h3;
        this.maxHealth = h1+h2+h3;
        this.health = maxHealth;
        this.lerpspeed = 2f;
        this.hl = hl;
        this.hr = hr;
    }
}