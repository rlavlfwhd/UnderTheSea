using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public BoxCollider2D movementArea;

    private CapsuleCollider2D playerCollider;

    void Start()
    {
        playerCollider = GetComponent<CapsuleCollider2D>();

        if (playerCollider == null)
        {
            Debug.LogWarning("Player does not have a CapsuleCollider2D! Please add one.");
        }
    }

    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (playerCollider == null)
        {
            return;
        }
                
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal, vertical, 0) * moveSpeed * Time.deltaTime;
        transform.Translate(move);
                
        RestrictMovementToBounds();
    }

    private void RestrictMovementToBounds()
    {
        Vector3 position = transform.position;

        float playerWidth = playerCollider.bounds.size.x;
        float playerHeight = playerCollider.bounds.size.y;

        float minX = movementArea.bounds.min.x + playerWidth / 2;
        float maxX = movementArea.bounds.max.x - playerWidth / 2;

        float minY = movementArea.bounds.min.y + playerHeight / 2;
        float maxY = movementArea.bounds.max.y - playerHeight / 2; 

        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);

        transform.position = position;
    }
}
