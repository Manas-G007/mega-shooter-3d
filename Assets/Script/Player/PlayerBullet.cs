using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Transform hitTransform = collision.transform;
        if (hitTransform.CompareTag("Enemy"))
        {
            if (hitTransform.GetComponent<EnemyHP>().HitPoint == 10)
            {
                hitTransform.GetComponent<EnemyHP>().UpdateHitPoint(10);
                Destroy(collision.gameObject);
            }
            else
            {
                hitTransform.GetComponent<EnemyHP>().UpdateHitPoint(10);
            }
        }
        Destroy(gameObject);
    }
}
