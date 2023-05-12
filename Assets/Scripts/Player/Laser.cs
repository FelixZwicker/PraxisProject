using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject laserPrefab;
    public Transform laserPoint;
    private GameObject laser;
    public Vector2 size;
    public LayerMask layerMask;
    public float laserDamage;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            laser = Instantiate(laserPrefab, laserPoint);
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Destroy(laser);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            CastLaser(laserDamage);
        }
    }

    public void CastLaser(float damage)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(laserPoint.position, new Vector2(size.x, size.y), layerMask);
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                StartCoroutine(col.GetComponent<Enemy_Health>().EnemyTakeDamage(damage));
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(laserPoint.position, new Vector3(size.x, size.y, 0));
    }
}
