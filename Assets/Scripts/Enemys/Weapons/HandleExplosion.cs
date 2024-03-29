using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleExplosion : MonoBehaviour
{
    public LayerMask layerMask;
    public float radius;
    public GameObject explosionPrefab;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public IEnumerator PlayExplosion()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosion.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(explosion.GetComponent<ParticleSystem>().main.duration);
        Destroy(explosion);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void CastSurrounding(int _damage)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);
        
        foreach (Collider2D col in colliders)
        {
            if (col.name == "Player")
            {
                StartCoroutine(player.GetComponent<PlayerController>().TakeDamage(_damage));
                
                Rigidbody2D rigid;
                if (col.TryGetComponent<Rigidbody2D>(out rigid))
                {
                    Vector3 dir = transform.position - col.transform.position;
                    
                    rigid.AddForce(dir.normalized * 10, ForceMode2D.Impulse);
                } 
            }
        }
    }
}
