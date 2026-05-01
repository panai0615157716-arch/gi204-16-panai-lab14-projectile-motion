using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform ShootPoint;
    [SerializeField] private GameObject Target;
    [SerializeField] private GameObject BulletPrefab;


    /*void Start()
    {
        
    }*/

    void Update()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red, 5f);

            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {
                Target.transform.position = new Vector2(hit.point.x, hit.point.y);
                Debug.Log($"Hit: {hit.collider.gameObject.name}");

                Vector2 projectileVelocity = CalculateProjectileVelocity(ShootPoint.position, hit.point, 1f);

                // แก้ไขจุดนี้: ใช้ GameObject เสกออกมาก่อน แล้วค่อย Get Rigidbody2D
                GameObject bulletObj = Instantiate(BulletPrefab, ShootPoint.position, Quaternion.identity);
                Rigidbody2D shootBullet = bulletObj.GetComponent<Rigidbody2D>();

                if (shootBullet != null)
                {
                    shootBullet.linearVelocity = projectileVelocity;
                }
            }
        }
    }   
    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 direction = target - origin;
        return new Vector2(
            direction.x / time,
            (direction.y / time) + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time
        );
    }








}
