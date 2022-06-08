using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController: MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    [SerializeField] private ParticleSystem punchParticle;
    [SerializeField] private string collectiblesLayerName;
    [SerializeField] private string portalLayerName;
    [SerializeField] private string enemyLayerName;

    private bool isGrounded;
    public bool IsGrounded { get; }
    private Vector3 playerVelocity;

    private CharacterController controller;
    private PlayerInput playerInput;
    private Animator animator;
    private Coin coin;

    private void Start()
    {
        coin = FindObjectOfType<Coin>();
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            animator.SetBool("land", true);
            animator.ResetTrigger("jump");
        }

        Vector2 input = playerInput.actions["Walk"].ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            animator.SetBool("walk", true);
        }
        else
        {
            animator.SetBool("walk", false);
        }

        // Changes the height position of the player..
        if (playerInput.actions["Jump"].triggered && isGrounded)
        {
            Jump();
        }

        if (playerInput.actions["Fire"].triggered)
        {
            Fire();
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void Fire()
    {
        punchParticle.Play();
        punchParticle.GetComponent<BoxCollider>().enabled = true;
        animator.SetTrigger("fire");
        AudioManager.instance.PlaySound("Fire");
        Invoke("ClosePunchCollider", 1f);
    }

    private void Jump()
    {
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        animator.SetTrigger("jump");
        AudioManager.instance.PlaySound("Jump");
        animator.SetBool("land", false);
    }

    private void ClosePunchCollider()
    {
        punchParticle.GetComponent<BoxCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.layer == LayerMask.NameToLayer(collectiblesLayerName))
        {
            Destroy(other.gameObject);
            AudioManager.instance.PlaySound("Coin");
            coin.UpdateCoin();
        }

        if (other.transform.gameObject.layer == LayerMask.NameToLayer(portalLayerName))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}