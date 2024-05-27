using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHP : MonoBehaviour
{
    private float hitPoint;
    private float maxHitPoint;
    [SerializeField]
    private GameObject healthBar;
    [SerializeField]
    private TextMesh hitPointNum;
    public float HitPoint { get => hitPoint; }
    // Start is called before the first frame update
    void Start()
    {
        maxHitPoint = 100f;
        hitPoint = maxHitPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHitPoint(float damage)
    {
        hitPoint -= damage;
        hitPoint = Mathf.Clamp(hitPoint, 0, maxHitPoint);
        hitPointNum.text = $"HP : {hitPoint}";
        Vector3 currentScale = healthBar.transform.localScale;
        currentScale.x -= 1f/damage;
        healthBar.transform.localScale = currentScale;

        Debug.Log(hitPoint);
    }
}
