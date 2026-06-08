using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class StoneObject : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            Task();
        }
    }
    private async UniTask Task()
    {
        await UniTask.Delay(1200);
        SceneManager.LoadScene(1);
    }

}
