using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using Cysharp.Threading.Tasks;

public class PlayerController : MonoBehaviour, IPlayerSetings, IMove
{
    public float speed { get; set; } = 5;
    public float jump { get; set; } = 13;

    public Rigidbody2D rb;

    [SerializeField] private SpriteRenderer spritePlayer;
    [SerializeField] private Sprite normalSprite; // вверх
    [SerializeField] private Sprite fallSprite;   // вниз
    [SerializeField] private Sprite deathSprite;  // смерть 


    IInput input;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = new KeyBordInput();
    }

    void Start()
    {
        spritePlayer = GetComponent<SpriteRenderer>();
    }

    public void LateUpdate()
    {
        float direction = input.Horizontal();

        Move(direction);
        UpdateSprite();
        FlipSprite(direction);
    }

    public void Move(float x)
    {
        rb.velocity = new Vector2(x * speed, rb.velocity.y);
    }

    private void UpdateSprite()
    {
        if (rb.velocity.y < -0.1f)
        {
            spritePlayer.sprite = fallSprite;
        }
        else
        {
            spritePlayer.sprite = normalSprite;
        }
    }

    private void FlipSprite(float direction)
    {
        if (direction > 0)
        {
            spritePlayer.flipX = false;
        }
        else if (direction < 0)
        {
            spritePlayer.flipX = true; 
        }
    }

    private async UniTaskVoid TaskVoid()
    {
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Vector3 jum = new Vector3(rb.velocity.x, jump, 0);
            rb.velocity = jum;
        }
        else if (collision.collider.CompareTag("Respawn"))
        {
            SceneManager.LoadScene(0);
        }

        if (collision.collider.CompareTag("DedObject"))
        {
           spritePlayer.enabled = false;
            TaskVoid();
        }
    }
}

class KeyBordInput : IInput
{
    public float Horizontal()
    {
        return Input.GetAxis("Horizontal");
    }
}

public interface IInput
{
    float Horizontal();
}

public interface IMove
{
    void Move(float direction);
}

public interface IPlayerSetings
{
    float speed { get; set; }
    float jump { get; set; }
}