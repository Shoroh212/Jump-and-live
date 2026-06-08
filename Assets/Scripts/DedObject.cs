using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DedObject : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float moveDistance = 3f;

    [Header("Sprites")]
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite animationSprite;

    [Header("Effects")]
    [SerializeField] private GameObject firstDestroy;
    [SerializeField] private GameObject secondDestroy;

    private SpriteRenderer spriteRenderer;

    private Vector3 startPosition;
    private float movementTime;
    private bool isPaused;

    private void Start()
    {
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();

        MoveLoop().Forget();
      //  AnimationLoop().Forget();

        this.OnTriggerEnter2DAsObservable()
            .Where(collision => collision.CompareTag("Player"))
            .Subscribe(_ => EndGame().Forget())
            .AddTo(this);
    }

    private async UniTaskVoid MoveLoop()
    {
        while (true)
        {
            if (!isPaused)
            {
                movementTime += Time.deltaTime;

                float x = Mathf.PingPong(movementTime * speed, moveDistance);

                transform.position = startPosition + Vector3.right * x;
            }

            await UniTask.Yield();
        }
    }

    /*
    private async UniTaskVoid AnimationLoop()
    {
        while (true)
        {
            await UniTask.Delay(12000);

            isPaused = true;
            spriteRenderer.sprite = animationSprite;

            // Здесь можно запускать Animator
            // animator.SetTrigger("Attack");

            await UniTask.Delay(3000);

            spriteRenderer.sprite = defaultSprite;
            isPaused = false;
        }
    }
    */

    private async UniTaskVoid EndGame()
    {
        GameObject first = Instantiate(firstDestroy, transform.position, Quaternion.identity);

        await UniTask.Delay(1000);

        Destroy(first);

        GameObject second = Instantiate(secondDestroy, transform.position, Quaternion.identity);

        await UniTask.Delay(1000);

        Destroy(second);

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