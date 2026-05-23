using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour , IPlayerSetings, IMove
{
    public float speed { get; set; } = 5;
    public float jump { get; set; } = 13;
    public Rigidbody2D rb;

    IInput input;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = new KeyBordInput();

    }


    public void LateUpdate()
    {
        float direction = input.Horizontal();
        Move(direction);
    }
    
    public void Move(float x)
    {
      
       // Vector3 movement = new Vector3(x * speed, rb.velocity.y, 0);
       rb.velocity = new Vector2(x * speed, rb.velocity.y);
     
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

   public float speed { get; set; }
    public float jump { get; set; }
   








}







/*
 * 
 * 
 * 
 * 
 *  public void Update()
    {
        float x = Input.GetAxis("Horizontal");
        move(x);
    }

 * 
 * 
 * 
 * 
 * 
 */