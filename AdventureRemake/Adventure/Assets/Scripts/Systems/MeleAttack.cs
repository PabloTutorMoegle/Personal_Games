using UnityEngine;

public class MeleAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float weaponRange;
    public LayerMask enemyLayers;


    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, enemyLayers);
        if (hits.Length > 0)
        {
            GameObject enemy = hits[0].gameObject;
            enemy.GetComponent<DamageSystem>().DoDamage(enemy);
        }
    }
}
