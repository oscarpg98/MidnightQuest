using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour {
    [SerializeField] private string sceneName;
    [SerializeField] private float fadeDuration;
    [SerializeField] private Image fadeImage;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject winScreen;

    private void Start() {
        StartCoroutine(FadeIn());
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(GameManager.Instance.Level + " " + GameManager.Instance.MaxLevel);
        if (other.CompareTag("PlayerHitbox") && GameManager.Instance.Level == GameManager.Instance.MaxLevel) {
            GameManager.Instance.Win();
        }
        else if (other.CompareTag("PlayerHitbox")) {
            StartCoroutine(FadeOutAndLoadScene(sceneName));
        }
    }

    private IEnumerator FadeIn() {
        Color color = fadeImage.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration) {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName) {
        Color color = fadeImage.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration) {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}
