using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{
    public GameObject laserTimerUI;
    public GameObject ammoUI;
    public int maxDistance;
    public Transform laserFirePoint;
    public LineRenderer lineRenderer;
    private Vector2 mousePos;
    public Camera cam;
    public float laserDamage;
    public float coolDownSpeed = 0.5f;
    public float laserMaxTimer = 8f;
    public float laserCurrentTimer;

    private Slider laserTimerSlider;
    private bool laserOverHeated = false;
    private Rigidbody2D rb;
    public ParticleSystem laserDamageEffect;

    void Start()
    {
        lineRenderer.enabled = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        laserCurrentTimer = laserMaxTimer;

        ammoUI.SetActive(false);
        laserTimerUI.SetActive(true);
        laserTimerSlider = laserTimerUI.GetComponent<Slider>();
        laserTimerSlider.minValue = 0;
        laserTimerSlider.maxValue = laserMaxTimer;
    }
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKey(KeyCode.Mouse0) && !laserOverHeated)
        {
            lineRenderer.enabled = true;
            laserCurrentTimer -= 1 * Time.deltaTime;
            ShootLaser();
        }
        else if(laserCurrentTimer < laserMaxTimer)
        {
            laserCurrentTimer += coolDownSpeed * Time.deltaTime;
        }
        else if(laserCurrentTimer >= laserMaxTimer)
        {
            laserCurrentTimer = laserMaxTimer;
            laserOverHeated = false;
        }

        if(laserCurrentTimer < 0)
        {
            laserOverHeated = true;
            lineRenderer.enabled = false;
            laserDamageEffect.Stop();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            lineRenderer.enabled = false;
            laserDamageEffect.Stop();
        }

        laserTimerSlider.value = laserCurrentTimer;
    }

    void ShootLaser()
    {
        
        Vector2 lookDir = mousePos - rb.position;
        Debug.Log(lookDir);
        if (Physics2D.Raycast(laserFirePoint.position, lookDir))
        {
            RaycastHit2D hit = Physics2D.Raycast(laserFirePoint.position, transform.up);
            if(!hit.transform.gameObject.CompareTag("Projectile"))
            {
                float distanceFirePointToHit = Vector2.Distance(new Vector2(laserFirePoint.position.x, laserFirePoint.position.y), hit.point);
                float distanceMousePosFirePoint = Vector2.Distance(mousePos, new Vector2(laserFirePoint.position.x, laserFirePoint.position.y));
                if (distanceFirePointToHit >= maxDistance)
                {
                    Vector2 endPoint = new Vector2(laserFirePoint.position.x, laserFirePoint.position.y) + Vector2.ClampMagnitude(lookDir, 10);
                    Draw2DRay(laserFirePoint.position, endPoint);
                }
                else if(distanceMousePosFirePoint <= distanceFirePointToHit)
                {
                    Vector2 endPoint = new Vector2(laserFirePoint.position.x, laserFirePoint.position.y) + lookDir;
                    Draw2DRay(laserFirePoint.position, endPoint);
                }
                else
                {
                    Draw2DRay(laserFirePoint.position, hit.point);
                    HandleLaserDamage(hit);
                }
            }
        }
    }

    private void HandleLaserDamage(RaycastHit2D _hit)
    {
        if (_hit.transform.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(_hit.transform.GetComponent<EnemyHealth>().EnemyTakeDamage(laserDamage));
            laserDamageEffect.transform.position = _hit.transform.position;
            laserDamageEffect.Play();
        }
        else
        {
            laserDamageEffect.Stop();
        }
    }

    void Draw2DRay(Vector2 _startPos, Vector2 _endPos)
    {
        lineRenderer.SetPosition(0, _startPos);
        lineRenderer.SetPosition(1, _endPos);
    }
}
