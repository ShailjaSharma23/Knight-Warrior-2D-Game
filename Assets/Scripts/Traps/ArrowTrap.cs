using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackcooldown;
    [SerializeField] private Transform Firepoint;
    [SerializeField] private GameObject[] arrows;
    private float cooldownTimer;

    [Header("SFX")]
    [SerializeField] private AudioClip arrowSound;

    private void Attack()
    {
        cooldownTimer = 0;

        SoundManager.instance.PlaySound(arrowSound);

        arrows[FindArrows()].transform.position = Firepoint.position;
        arrows[FindArrows()].GetComponent<ArrowProjectile>().ActivateProjectile();
    }

    private int FindArrows()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= attackcooldown)
        {
            Attack();
        }
    }
}
