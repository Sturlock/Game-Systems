using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private class ActiveTrail
    {
        public LineRenderer renderer;
        public Vector3 direction;
        public float remainingTime;
    }

    public Weapon_sObj weaponsObj;

    [Header("Target Settings")]
    public GameObject Player;

    public Camera enCam;

    [SerializeField] private Transform target;

    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private float turnSpeed = 5f;

    [SerializeField] private float timer = 0f;
    [SerializeField] private float achivTime;

    private float distanceToTarget = Mathf.Infinity;
    private bool isProvoked = false;
    private List<ActiveTrail> m_ActiveTrails = new List<ActiveTrail>();
    public Transform endPoint;

    private void OnDrawGizmosSelected ()
    {
        //Display the Chase Radius of the enemy
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        target = Player.transform;
        achivTime = weaponsObj.fireRate;
        enCam = GetComponent<Camera>();
    }

    private void Update()
    {
        
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
        
        Vector3[] pos = new Vector3[2];
        for (int i = 0; i < m_ActiveTrails.Count; ++i)
        {
            var activeTrail = m_ActiveTrails[i];

            activeTrail.renderer.GetPositions(pos);
            activeTrail.remainingTime -= Time.deltaTime;

            pos[0] += activeTrail.direction * 70.0f * Time.deltaTime;
            pos[1] += activeTrail.direction * 70.0f * Time.deltaTime;

            m_ActiveTrails[i].renderer.SetPositions(pos);

            if (m_ActiveTrails[i].remainingTime <= 0.0f)
            {
                m_ActiveTrails[i].renderer.gameObject.SetActive(false);
                m_ActiveTrails.RemoveAt(i);
                i--;
            }
        }
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    private void EngageTarget()
    {
        FaceTarget();
        if (distanceToTarget <= chaseRange)
        {
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        timer += Time.deltaTime;
        Shoot();
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.fixedDeltaTime * turnSpeed);
        //transform.rotation = our rotation, target rotation, speed
    }

    public Vector3 GetCorrectedMuzzlePlace()
    {
        Vector3 position = endPoint.position;

        position = Controller.Instance.WeaponCamera.WorldToScreenPoint(position);
        position = Controller.Instance.MainCamera.ScreenToWorldPoint(position);

        return position;
    }

    void Shoot()
    {
        if (timer < achivTime) return;
        timer = 0f;
        //compute the ratio of our spread angle over the fov to know in viewport space what is the possible offset from center
        float spreadRatio = weaponsObj.advancedSettings.spreadAngle / enCam.fieldOfView;

        Vector2 spread = spreadRatio * Random.insideUnitCircle;

        RaycastHit hit;
        Ray r = enCam.ViewportPointToRay(Vector3.one * 0.5f + (Vector3)spread);
        Vector3 hitPosition = r.origin + r.direction * 200.0f;

        if (Physics.Raycast(r, out hit, 1000.0f, ~(1 << 9), QueryTriggerInteraction.Ignore))
        {
            Renderer renderer = hit.collider.GetComponentInChildren<Renderer>();
            ImpactManager.Instance.PlayImpact(hit.point, hit.normal, renderer == null ? null : renderer.sharedMaterial);

            //if too close, the trail effect would look weird if it arced to hit the wall, so only correct it if far
            if (hit.distance > 5.0f)
                hitPosition = hit.point;

            //this is a target
            if (hit.collider.gameObject.layer == 10 || hit.collider.gameObject.layer == 14)
            {
                Target target = hit.collider.gameObject.GetComponent<Target>();
                target.Got(weaponsObj.damage);
            }
        }

        if (weaponsObj.PrefabRayTrail != null)
        {
            var pos = new Vector3[] { GetCorrectedMuzzlePlace(), hitPosition };
            var trail = PoolSystem.Instance.GetInstance<LineRenderer>(weaponsObj.PrefabRayTrail);
            trail.gameObject.SetActive(true);
            trail.SetPositions(pos);
            m_ActiveTrails.Add(new ActiveTrail()
            {
                remainingTime = 0.3f,
                direction = (pos[1] - pos[0]).normalized,
                renderer = trail
            });
        }
    }
}