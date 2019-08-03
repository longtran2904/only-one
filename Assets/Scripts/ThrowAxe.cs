using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAxe : MonoBehaviour
{
    public GameObject weapon;
    private bool hasWeapon = true;
    private Rigidbody2D weaponRb;
    //public float throwPower;
    //private Weapon weaponScript;
    public Transform firePoint;
    [SerializeField] public static bool isPulling = false;

    private float time = 0.0f;
    public Transform curve_pos;
    public Transform target;
    private Vector3 old_pos;

    // Start is called before the first frame update
    void Start()
    {
        weaponRb = weapon.GetComponent<Rigidbody2D>();
        //weaponScript = weapon.GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && hasWeapon)
        {
            ThrowWeapon();
        }

        if (Input.GetButtonDown("Fire2") && !hasWeapon)
        {
            PullWeapon();
        }

        if (isPulling)
        {
            if (time < 1.0f)
            {
                Debug.Log(weaponRb.isKinematic);
                weaponRb.position = GetQuadraticCurvePoint(time, old_pos, curve_pos.position, target.position);
                //weaponRb.rotation = Quaternion.Slerp(weaponRb.transform.position, target.rotation, 50 * Time.deltaTime);                
                //Debug.Log(old_pos + "a");
                //Debug.Log(curve_pos.position + "b");
                //Debug.Log(target.position + "c");
                //Debug.Log(GetQuadraticCurvePoint(time, old_pos, curve_pos.position, target.position) + "d");
                //Debug.Log(weaponRb.position);
                time += Time.deltaTime;
            }
            else
            {
                weaponRb.isKinematic = false;
                isPulling = false;
                hasWeapon = true;
                //Destroy(weapon);
                Debug.Log("catched weapon");
            }
        }
    }

    void ThrowWeapon()
    {
        weapon = Instantiate(weapon, firePoint.position, firePoint.rotation);
        hasWeapon = false;
        /*weaponScript.activated = true;
        weaponRb.isKinematic = false;
        weaponRb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        weapon.transform.parent = null;
        weapon.transform.eulerAngles = new Vector3(0, -90 + transform.eulerAngles.y, 0);
        weapon.transform.position += transform.right / 5;
        weaponRb.AddForce(Camera.main.transform.forward * throwPower + transform.up * 2, ForceMode2D.Impulse);*/
    }

    void PullWeapon()
    {
        time = 0.0f;
        old_pos = weapon.transform.position;
        isPulling = true;        
        weaponRb.velocity = Vector2.zero;
        weaponRb.isKinematic = true;
    }

    public Vector3 GetQuadraticCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = (uu * p0) + (2 * u * t * p1) + (tt * p2);
        return p;
    }
}
