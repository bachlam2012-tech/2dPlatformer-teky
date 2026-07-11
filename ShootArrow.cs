// using System.Collections;
// using System.Collections.Generic;
// using Unity.Mathematics;
// using UnityEngine;

// public class ShootArrow : MonoBehaviour
// {
//     private float shootingCD = 1;
//     private float CDTimer;
//     public bool isShooting;
//     public GameObject Arrow;
//     public GameObject Player;

//     public float shootForce = 20f;

//     // Start is called before the first frame update
//     void Start()
//     {

//     }

//     // Update is called once per frame
//     void Update()
//     {
//         CDTimer -= Time.deltaTime;
//         if (Input.GetKeyDown(KeyCode.Mouse1) && CDTimer <= 0 && isShooting == false)
//         {
//             // Instantiate(Arrow, Player.transform.position, quaternion.identity);
//             // isShooting = true;
//             // CDTimer = shootingCD;
//             GameObject arrow = Instantiate(
//                Arrow,
//                Player.transform.position,
//                Player.transform.rotation);

//             Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();

//             rb.AddForce(Player.transform.forward * shootForce, ForceMode2D.Impulse);

//             CDTimer = shootingCD;


//         }
//     }
// }
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    public GameObject Arrow;
    public Transform shootPoint;

    public float shootForce = 15f;
    public float arrowAngleOffset = -45f;

    private float shootingCD = 1f;
    private float CDTimer;

    void Update()
    {
        CDTimer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(1) && CDTimer <= 0f)
        {
            Shoot();
            CDTimer = shootingCD;
        }
    }

    void Shoot()
    {
        Vector3 mousePos =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePos.z = 0f;

        Vector2 direction =
            ((Vector2)mousePos - (Vector2)shootPoint.position).normalized;

        float angle =
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        GameObject arrow = Instantiate(
            Arrow,
            shootPoint.position,
            Quaternion.Euler(0, 0, angle + arrowAngleOffset)
        );

        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.AddForce(direction * shootForce, ForceMode2D.Impulse);
        }

        Destroy(arrow, 5f);
    }
}