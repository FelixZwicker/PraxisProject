using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public Animator animator;
    public LayerMask layerMask;
    public float timebetweenSlashes;
    public int damage;
    public Transform firePoint;
    public Vector2 size;
    public float delay;

    private bool canSlash;

    void Start()
    {
        canSlash = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canSlash == true)
        {
            StartCoroutine(Slash());
        }
    }

    IEnumerator Slash()
    {
        animator.Play("PlayerMeleeAttack");
        yield return new WaitForSeconds(delay);
        Debug.Log("slash");
        CastPlayerSwordSurrounding(damage);
        canSlash = false;
        yield return new WaitForSeconds(timebetweenSlashes);
        canSlash = true;
    }

    public void CastPlayerSwordSurrounding(int damage)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(firePoint.position, new Vector2(size.x, size.y), layerMask);
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                StartCoroutine(col.GetComponent<EnemyHealth>().EnemyTakeDamage(damage));
            }
        }
    }
}
