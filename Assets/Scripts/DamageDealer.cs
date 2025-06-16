using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int enemyDamage = 10;

    public int GetDamage()
    {
        return enemyDamage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
