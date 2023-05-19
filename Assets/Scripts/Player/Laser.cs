using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int maxDistance;
    public Transform laserFirePoint;
    public LineRenderer lineRenderer;
    private Vector2 mousePos;
    public Camera cam;
    public float laserDamage;

    private Rigidbody2D rb;
    public ParticleSystem laserDamageEffect;

    void Start()
    {
        lineRenderer.enabled = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            lineRenderer.enabled = true;
            ShootLaser();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            lineRenderer.enabled = false;
            laserDamageEffect.Stop();
        }
    }

    void ShootLaser()
    {
        Vector2 lookDir = mousePos - rb.position;
        if (Physics2D.Raycast(laserFirePoint.position, lookDir))
        {
            RaycastHit2D hit = Physics2D.Raycast(laserFirePoint.position, transform.up);
            if(!hit.transform.gameObject.CompareTag("Projectile"))
            {
                if (Vector2.Distance(new Vector2(laserFirePoint.position.x, laserFirePoint.position.y), hit.point) > maxDistance)
                {
                    Vector2 endPoint = new Vector2(laserFirePoint.position.x, laserFirePoint.position.y) + Vector2.ClampMagnitude(lookDir, 10);
                    Draw2DRay(laserFirePoint.position, endPoint);
                }
                else
                {
                    Draw2DRay(laserFirePoint.position, hit.point);
                    HandleLaserDamage(hit);
                }
                Debug.Log(hit.transform.gameObject.name);
            }
        }
    }

    private void HandleLaserDamage(RaycastHit2D hit)
    {
        if (hit.transform.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(hit.transform.GetComponent<Enemy_Health>().EnemyTakeDamage(laserDamage));
            laserDamageEffect.transform.position = hit.transform.position;
            laserDamageEffect.Play();
        }
        else
        {
            laserDamageEffect.Stop();
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
