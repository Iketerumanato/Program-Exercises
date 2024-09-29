using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Rigidbody PlayerRig;
    [SerializeField] float moveSpeed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        PlayerRig.velocity = new Vector2(moveHorizontal * moveSpeed, PlayerRig.velocity.y);
    }
}
