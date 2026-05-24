
using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DedObject : MonoBehaviour
{

    [SerializeField] private float speed = 2f;
    [SerializeField] private Sprite first_sprite;
    [SerializeField] private Sprite second_sprite;
    [SerializeField] private Sprite defolt;
    Vector3 startpos;
    private bool moveHorizontal = true;

    [SerializeField] private GameObject first_destroy;
    [SerializeField] private GameObject second_destroy;


    SpriteRenderer spriteRenderer;

  

    private void Start()
    {
        startpos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();

        EnemyLoop().Forget();
        Animation();
        StopAnimation().Forget();

        this.OnTriggerEnter2DAsObservable().Where(collision => collision.CompareTag("Player"))
           .Subscribe(_ => EndGame())
           .AddTo(this);

    }


    private async UniTaskVoid EnemyLoop()
    {
        while (true)
        {
            if (moveHorizontal)
            {
                spriteRenderer.sprite = defolt;
                Vector3 horizontal =
                    transform.right *
                    Mathf.PingPong(Time.time * speed, 3);

                transform.position = startpos + horizontal;
            }
            else
            {
                Vector3 vertical =
                    transform.up *
                    Mathf.PingPong(Time.time * 2.5f, 0.5f);

                transform.position = startpos +  vertical;
            }

            await UniTask.Yield();
        }
    }

    private async UniTask Animation()
    {
        await UniTask.Delay(11800);
        moveHorizontal = false;
        spriteRenderer.sprite = first_sprite;
        





    }

    private async UniTask StopAnimation()
    {
        await UniTask.Delay(23600);
        moveHorizontal = true;
        spriteRenderer.sprite = defolt;
    }





     async UniTask EndGame()
    {
       
        Instantiate(first_destroy);
        await UniTask.Delay(1000);
        Destroy(first_destroy);
        Instantiate(second_destroy);
        await UniTask.Delay(1000);
        Destroy(second_destroy) ;



        await UniTask.Delay(2000);  

        SceneManager.LoadScene(0);
        
    }
}


/*
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 *  anim.enabled = false;
            transform.position = vec + transform.right * Mathf.PingPong(Time.time * speed, 3);
            bobm_object.sprite = idleSprite;
 
 * 
 * 
 * 
 *   IEnumerator MyCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(8);
            isPaused = true;

            yield return new WaitForSeconds(3);
            isPaused = false;
        }
    }
 * 
 * 
 * 
 * 
 * 
 * 
 *   void Start()
    {
        vec = transform.position;
       // StartCoroutine(MyCoroutine());
        bobm_object = GetComponent<SpriteRenderer>();   

    }
 * 
*/