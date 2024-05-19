using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField]
    private TMP_Text notificationText;
    [SerializeField]
    private float notificationDuration = 2f;

    private Coroutine currentCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            notificationText.gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateText(string text)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        notificationText.text = text;
        notificationText.gameObject.SetActive(true);

        currentCoroutine = StartCoroutine(HideNotificationAfterDelay(notificationDuration));
    }

    private IEnumerator HideNotificationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        notificationText.gameObject.SetActive(false);
        currentCoroutine = null;
    }
}
