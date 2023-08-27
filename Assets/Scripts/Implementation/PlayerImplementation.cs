using HH;
using UnityEngine;

public class PlayerImplementation: MonoBehaviour
{
    public bool isPlayer = true;
    Movement playerMovement;
    private void Start()
    {
        if (isPlayer)
        {
            playerMovement = new IPlayerMovement(gameObject);
        }
        else
        {
            playerMovement = new IAIMovement(gameObject);
        }
    }
    private void Update()
    {
        playerMovement.MoveTo();
    }
}