using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform movePoint;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private LayerMask treasureMask;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float tilePerMove;

    private float inputX, inputY;
    private bool canMove;

    private void Start()
    {
        movePoint.parent = null;
        canMove = true;
    }

    private void Update()
    {
        GetInput();

        HandleMovement();
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, movePoint.position) <= .05f)
        {
            return;
        }
        
        transform.position = Vector2.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
    }

    private void GetInput()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
    }

    private void HandleMovement()
    {  
        if (Vector2.Distance(transform.position, movePoint.position) > .05f)
        {
            return;
        }

        if (!canMove)
        {
            return;
        }

        if (Mathf.Abs(inputX) == 1 || Mathf.Abs(inputY) == 1)
        {
            Vector3 direction = new Vector3(inputX, inputY, 0f);

            if (Physics2D.OverlapCircle(movePoint.position + direction * tilePerMove, .2f, obstacleMask))
            {
                return;
            }

            if (Physics2D.OverlapCircle(movePoint.position + direction * tilePerMove, .2f, treasureMask))
            {
                Debug.Log("Find Treasure!");
                canMove = false;
                return;
            }

            movePoint.position += direction * tilePerMove;

            GameManager.Instance.DescreaseSteps();
        }
    }
}
