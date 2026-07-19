using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAI : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    public GameObject[] waypoint;
    int nextWayPoint = 1;
    float distToPoint;
    bool facingright;

    // RAYCAST
    public float rayDistance = 5f;
    public LayerMask playerLayer;

    // TỐC ĐỘ LAO XUỐNG
    public float diveSpeed = 8f;

    // COOLDOWN GIỮA MỖI LẦN LAO
    public float diveCooldown = 3f;

    bool isDiving = false;

    // Ong có được phép lao xuống không
    bool canDive = true;

    // Vị trí ong sẽ lao tới
    Vector3 diveTarget;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Nếu đang lao xuống
        if (isDiving == true)
        {
            DiveAttack();
        }
        else
        {
            // Bay theo waypoint
            Move();

            // Nếu hết cooldown thì mới kiểm tra Player
            if (canDive == true)
            {
                PlayerCheck();
            }
        }
    }


    void Move()
    {
        distToPoint = Vector3.Distance(
            transform.position,
            waypoint[nextWayPoint].transform.position
        );

        transform.position = Vector3.MoveTowards(
            transform.position,
            waypoint[nextWayPoint].transform.position,
            moveSpeed * Time.deltaTime
        );

        if (distToPoint < 0.05f)
        {
            transform.position = waypoint[nextWayPoint].transform.position;
            TakeTurn();
        }
    }


    void TakeTurn()
    {
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.z += waypoint[nextWayPoint].transform.eulerAngles.z;
        transform.eulerAngles = currentRotation;

        ChooseNextWayPoint();
        flip();
    }


    void ChooseNextWayPoint()
    {
        nextWayPoint++;

        if (nextWayPoint == waypoint.Length)
        {
            nextWayPoint = 0;
        }
    }


    void flip()
    {
        facingright = !facingright;
        transform.Rotate(0, 180, 0);
    }


    void PlayerCheck()
    {
        // Raycast xuống dưới
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            rayDistance,
            playerLayer
        );

        // Vẽ Raycast
        Debug.DrawRay(
            transform.position,
            Vector2.down * rayDistance,
            Color.red
        );

        // Nếu phát hiện Player
        if (hit.collider != null)
        {
            // Bắt đầu lao xuống
            isDiving = true;

            // Khóa không cho lao tiếp
            canDive = false;

            // Lưu vị trí Player
            diveTarget = hit.collider.transform.position;
        }
    }


    void DiveAttack()
    {
        // Lao xuống vị trí Player
        transform.position = Vector3.MoveTowards(
            transform.position,
            diveTarget,
            diveSpeed * Time.deltaTime
        );

        // Khi lao đến vị trí Player
        if (Vector3.Distance(transform.position, diveTarget) < 0.05f)
        {
            transform.position = diveTarget;

            // Kết thúc lao
            isDiving = false;

            // Bắt đầu cooldown 3 giây
            StartCoroutine(DiveCooldown());


        }
    }


    IEnumerator DiveCooldown()
    {
        // Chờ 3 giây
        yield return new WaitForSeconds(diveCooldown);

        // Sau 3 giây mới được lao tiếp
        canDive = true;


    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(
            transform.position,
            transform.position + Vector3.down * rayDistance
        );
    }
}