using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public LayerMask layerMask;
    public float timebetweenSlashes;
    public int damage;
    public Transform firePoint;
    public Vector2 size;

    private bool canSlash;

    // Start is called before the first frame update
    void Start()
    {
        canSlash = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canSlash == true)
        {
            Slash();
        }
    }

    private void Slash()
    {
        Debug.Log("slash");
        CastPlayerSwordSurrounding(damage);
        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        canSlash = false;
        yield return new WaitForSeconds(timebetweenSlashes);
        canSlash = true;
    }

    public void CastPlayerSwordSurrounding(int damage)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(firePoint.position, new Vector2(1, 2), layerMask);
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
        Gizmos.DrawWireCube(firePoint.position, new Vector3(size.x, size.y, 0));
    }
}