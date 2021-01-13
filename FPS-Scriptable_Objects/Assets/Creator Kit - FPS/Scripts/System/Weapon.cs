using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Weapon : MonoBehaviour
{
    public enum WeaponState
    {
        Idle,
        Firing,
        Reloading
    }
    static RaycastHit[] s_HitInfoBuffer = new RaycastHit[8];

    public Weapon_sObj weaponsObj;

    [Header("Visual Display")]
    public AmmoDisplay ammoDisplay;

    public Transform EndPoint; 
    
    public bool triggerDown
    {
        get { return m_TriggerDown; }
        set 
        { 
            m_TriggerDown = value;
            if (!m_TriggerDown) m_ShotDone = false;
        }
    }

    public WeaponState CurrentState => m_CurrentState;
    public int ClipContent => m_ClipContent;
    public Controller Owner => m_Owner;

    Controller m_Owner;
    
    Animator m_Animator;
    WeaponState m_CurrentState;
    bool m_ShotDone;
    float m_ShotTimer = -1.0f;
    bool m_TriggerDown;
    int m_ClipContent;

    AudioSource m_Source;

    Vector3 m_ConvertedMuzzlePos;

    class ActiveTrail
    {
        public LineRenderer renderer;
        public Vector3 direction;
        public float remainingTime;
    }
    
    List<ActiveTrail> m_ActiveTrails = new List<ActiveTrail>();
    
    Queue<Projectile> m_ProjectilePool = new Queue<Projectile>();
    
    int fireNameHash = Animator.StringToHash("fire");
    int reloadNameHash = Animator.StringToHash("reload");     

    void Awake()
    {
        m_Animator = GetComponentInChildren<Animator>();
        m_Source = GetComponentInChildren<AudioSource>();
        m_ClipContent = weaponsObj.clipSize;

        if (weaponsObj.PrefabRayTrail != null)
        {
            const int trailPoolSize = 16;
            PoolSystem.Instance.InitPool(weaponsObj.PrefabRayTrail, trailPoolSize);
        }

        if (weaponsObj.projectilePrefab != null)
        {
            //a minimum of 4 is useful for weapon that have a clip size of 1 and where you can throw a second
            //or more before the previous one was recycled/exploded.
            int size = Mathf.Max(4, weaponsObj.clipSize) * weaponsObj.advancedSettings.projectilePerShot;
            for (int i = 0; i < size; ++i)
            {
                Projectile p = Instantiate(weaponsObj.projectilePrefab);
                p.gameObject.SetActive(false);
                m_ProjectilePool.Enqueue(p);
            }
        }
    }

    public void PickedUp(Controller c)
    {
        m_Owner = c;
    }

    public void PutAway()
    {
        m_Animator.WriteDefaultValues();
        
        for (int i = 0; i < m_ActiveTrails.Count; ++i)
        {
            var activeTrail = m_ActiveTrails[i];
            m_ActiveTrails[i].renderer.gameObject.SetActive(false);
        }
        
        m_ActiveTrails.Clear();
    }

    public void Selected()
    {
        int ammoRemaining = m_Owner.GetAmmo(weaponsObj.ammoType);
        
        //gun get disabled when ammo is == 0 and there is no more ammo in the clip, so this allow to re-enable it if we
        //grabbed ammo since last time we switched
        gameObject.SetActive(ammoRemaining != 0 || m_ClipContent != 0);
        
        if(weaponsObj.FireAnimationClip != null)
            m_Animator.SetFloat("fireSpeed", weaponsObj.FireAnimationClip.length / weaponsObj.fireRate);
        
        if(weaponsObj.ReloadAnimationClip != null)
            m_Animator.SetFloat("reloadSpeed", weaponsObj.ReloadAnimationClip.length / weaponsObj.reloadTime);
        
        m_CurrentState = WeaponState.Idle;

        triggerDown = false;
        m_ShotDone = false;
        
        WeaponInfoUI.Instance.UpdateWeaponName(this);
        WeaponInfoUI.Instance.UpdateClipInfo(this);
        WeaponInfoUI.Instance.UpdateAmmoAmount(m_Owner.GetAmmo(weaponsObj.ammoType));
        
        if(ammoDisplay)
            ammoDisplay.UpdateAmount(m_ClipContent, weaponsObj.clipSize);

        if (m_ClipContent == 0 && ammoRemaining != 0)
        { 
            //this can only happen if the weapon ammo reserve was empty and we picked some since then. So directly
            //reload the clip when weapon is selected          
            int chargeInClip = Mathf.Min(ammoRemaining, weaponsObj.clipSize);
            m_ClipContent += chargeInClip;        
            if(ammoDisplay)
                ammoDisplay.UpdateAmount(m_ClipContent, weaponsObj.clipSize);        
            m_Owner.ChangeAmmo(weaponsObj.ammoType, -chargeInClip);       
            WeaponInfoUI.Instance.UpdateClipInfo(this);
        }
        
        m_Animator.SetTrigger("selected");
    }

    public void Fire()
    {
        if (m_CurrentState != WeaponState.Idle || m_ShotTimer > 0 || m_ClipContent == 0)
            return;
        
        m_ClipContent -= 1;
        
        m_ShotTimer = weaponsObj.fireRate;

        if(ammoDisplay)
            ammoDisplay.UpdateAmount(m_ClipContent, weaponsObj.clipSize);
        
        WeaponInfoUI.Instance.UpdateClipInfo(this);

        //the state will only change next frame, so we set it right now.
        m_CurrentState = WeaponState.Firing;
        
        m_Animator.SetTrigger("fire");

        m_Source.pitch = Random.Range(0.7f, 1.0f);
        m_Source.PlayOneShot(weaponsObj.FireAudioClip);
        
        CameraShaker.Instance.Shake(0.2f, 0.05f * weaponsObj.advancedSettings.screenShakeMultiplier);

        if (weaponsObj.weaponType == WeaponType.Raycast)
        {
            for (int i = 0; i < weaponsObj.advancedSettings.projectilePerShot; ++i)
            {
                RaycastShot();
            }
        }
        else
        {
            ProjectileShot();
        }
    }


    public void RaycastShot()
    {

        //compute the ratio of our spread angle over the fov to know in viewport space what is the possible offset from center
        float spreadRatio = weaponsObj.advancedSettings.spreadAngle / Controller.Instance.MainCamera.fieldOfView;

        Vector2 spread = spreadRatio * Random.insideUnitCircle;
        
        RaycastHit hit;
        Ray r = Controller.Instance.MainCamera.ViewportPointToRay(Vector3.one * 0.5f + (Vector3)spread);
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

    void ProjectileShot()
    {
        for (int i = 0; i < weaponsObj.advancedSettings.projectilePerShot; ++i)
        {
            float angle = Random.Range(0.0f, weaponsObj.advancedSettings.spreadAngle * 0.5f);
            Vector2 angleDir = Random.insideUnitCircle * Mathf.Tan(angle * Mathf.Deg2Rad);

            Vector3 dir = EndPoint.transform.forward + (Vector3)angleDir;
            dir.Normalize();

            var p = m_ProjectilePool.Dequeue();
            
            p.gameObject.SetActive(true);
            p.Launch(this, dir, weaponsObj.projectileLaunchForce);
        }
    }

    //For optimization, when a projectile is "destroyed" it is instead disabled and return to the weapon for reuse.
    public void ReturnProjecticle(Projectile p)
    {
        m_ProjectilePool.Enqueue(p);
    }

    public void Reload()
    {
        if (m_CurrentState != WeaponState.Idle || m_ClipContent == weaponsObj.clipSize)
            return;

        int remainingBullet = m_Owner.GetAmmo(weaponsObj.ammoType);

        if (remainingBullet == 0)
        {
            //No more bullet, so we disable the gun so it's not displayed anymore and change weapon
            gameObject.SetActive(false);
            return;
        }


        if (weaponsObj.ReloadAudioClip != null)
        {
            m_Source.pitch = Random.Range(0.7f, 1.0f);
            m_Source.PlayOneShot(weaponsObj.ReloadAudioClip);
        }

        int chargeInClip = Mathf.Min(remainingBullet, weaponsObj.clipSize - m_ClipContent);
     
        //the state will only change next frame, so we set it right now.
        m_CurrentState = WeaponState.Reloading;
        
        m_ClipContent += chargeInClip;
        
        if(ammoDisplay)
            ammoDisplay.UpdateAmount(m_ClipContent, weaponsObj.clipSize);
        
        m_Animator.SetTrigger("reload");
        
        m_Owner.ChangeAmmo(weaponsObj.ammoType, -chargeInClip);
        
        WeaponInfoUI.Instance.UpdateClipInfo(this);
    }

    void Update()
    {
        UpdateControllerState();        
        
        if (m_ShotTimer > 0)
            m_ShotTimer -= Time.deltaTime;

        Vector3[] pos = new Vector3[2];
        for (int i = 0; i < m_ActiveTrails.Count; ++i)
        {
            var activeTrail = m_ActiveTrails[i];
            
            activeTrail.renderer.GetPositions(pos);
            activeTrail.remainingTime -= Time.deltaTime;

            pos[0] += activeTrail.direction * 50.0f * Time.deltaTime;
            pos[1] += activeTrail.direction * 50.0f * Time.deltaTime;
            
            m_ActiveTrails[i].renderer.SetPositions(pos);
            
            if (m_ActiveTrails[i].remainingTime <= 0.0f)
            {
                m_ActiveTrails[i].renderer.gameObject.SetActive(false);
                m_ActiveTrails.RemoveAt(i);
                i--;
            }
        }
    }

    void UpdateControllerState()
    {
        m_Animator.SetFloat("speed", m_Owner.Speed);
        m_Animator.SetBool("grounded", m_Owner.Grounded);
        
        var info = m_Animator.GetCurrentAnimatorStateInfo(0);

        WeaponState newState;
        if (info.shortNameHash == fireNameHash)
            newState = WeaponState.Firing;
        else if (info.shortNameHash == reloadNameHash)
            newState = WeaponState.Reloading;
        else
            newState = WeaponState.Idle;

        if (newState != m_CurrentState)
        {
            var oldState = m_CurrentState;
            m_CurrentState = newState;
            
            if (oldState == WeaponState.Firing)
            {//we just finished firing, so check if we need to auto reload
                if(m_ClipContent == 0)
                    Reload();
            }
        }

        if (triggerDown)
        {
            if (weaponsObj.triggerType == TriggerType.Manual)
            {
                if (!m_ShotDone)
                {
                    m_ShotDone = true;
                    Fire();
                }
            }
            else
                Fire();
        }
    }
    
    /// <summary>
    /// This will compute the corrected position of the muzzle flash in world space. Since the weapon camera use a
    /// different FOV than the main camera, using the muzzle spot to spawn thing rendered by the main camera will appear
    /// disconnected from the muzzle flash. So this convert the muzzle post from
    /// world -> view weapon -> clip weapon -> inverse clip main cam -> inverse view cam -> corrected world pos
    /// </summary>
    /// <returns></returns>
    public Vector3 GetCorrectedMuzzlePlace()
    {
        Vector3 position = EndPoint.position;

        position = Controller.Instance.WeaponCamera.WorldToScreenPoint(position);
        position = Controller.Instance.MainCamera.ScreenToWorldPoint(position);

        return position;
    }
}

public class AmmoTypeAttribute : PropertyAttribute
{
    
}

public abstract class AmmoDisplay : MonoBehaviour
{
    public abstract void UpdateAmount(int current, int max);
}

#if UNITY_EDITOR


[CustomPropertyDrawer(typeof(AmmoTypeAttribute))]
public class AmmoTypeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        AmmoDatabase ammoDB = GameDatabase.Instance.ammoDatabase;

        if (ammoDB.entries == null || ammoDB.entries.Length == 0)
        {
            EditorGUI.HelpBox(position, "Please define at least 1 ammo type in the Game Database", MessageType.Error);
        }
        else
        {
            int currentID = property.intValue;
            int currentIdx = -1;

            //this is pretty ineffective, maybe find a way to cache that if prove to take too much time
            string[] names = new string[ammoDB.entries.Length];
            for (int i = 0; i < ammoDB.entries.Length; ++i)
            {
                names[i] = ammoDB.entries[i].name;
                if (ammoDB.entries[i].id == currentID)
                    currentIdx = i;
            }

            EditorGUI.BeginChangeCheck();
            int idx = EditorGUI.Popup(position, "Ammo Type", currentIdx, names);
            if (EditorGUI.EndChangeCheck())
            {
                property.intValue = ammoDB.entries[idx].id;
            }
        }
    }
}

#endif