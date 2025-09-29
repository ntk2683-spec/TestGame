using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashSpeed = 12f;       // tốc độ dash
    [SerializeField] private float dashDuration = 0.2f;   // thời gian dash (giây)
    public Joystick joystick;

    private float moveHorizontal;
    private float moveVertical;
    private Vector2 movement;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public static PlayerControl Instance;
    [SerializeField] private GameObject shield;

    private bool isDashing = false;

    void Start()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
        animator = rb.GetComponent<Animator>();
        shield.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (isDashing) return; // Khi đang dash thì không xử lý move thường

        moveHorizontal = joystick.Horizontal;
        moveVertical = joystick.Vertical;
        movement = new Vector2(moveHorizontal, moveVertical) * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + movement);

        // Flip sprite
        if (movement.x < 0)
            spriteRenderer.flipX = true;
        else if (movement.x > 0)
            spriteRenderer.flipX = false;

        // Animation chạy
        animator.SetBool("isRun", movement != Vector2.zero);
    }

    // Gọi hàm này bằng Button UI
    public void DashButton()
    {
        if (!isDashing)
            StartCoroutine(Dash());
    }

    private IEnumerator Dash()
    {
        isDashing = true;

        // Hướng dash theo joystick hoặc hướng đang facing
        Vector2 dashDirection = new Vector2(joystick.Horizontal, joystick.Vertical);
        if (dashDirection == Vector2.zero)
        {
            dashDirection = spriteRenderer.flipX ? Vector2.left : Vector2.right;
        }

        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            rb.MovePosition(rb.position + dashDirection.normalized * dashSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }

        isDashing = false;
    }
    public void Shield()
    {
        // Nếu Shield đang active thì tắt, nếu tắt thì bật
        shield.SetActive(!shield.activeSelf);
    }

}
