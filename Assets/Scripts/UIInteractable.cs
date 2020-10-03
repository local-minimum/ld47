using UnityEngine.UI;
using UnityEngine;

public class UIInteractable : MonoBehaviour
{
    Canvas canvas;

    [SerializeField]
    Image keyHint;

    [SerializeField]
    Image interactHint;

    [SerializeField, Range(0, 40)]
    float rotationSpeed = 10f;

    static UIInteractable _instance;

    public static void Place(Transform target)
    {
        _instance.transform.SetParent(target);
        _instance.transform.localPosition = Vector3.zero;
        _instance.canvas.enabled = true;
    }

    public static bool canInteract {
        set
        {
            _instance.keyHint.gameObject.SetActive(value);
        }
    }

    public bool showingKey {
        get
        {
            return keyHint.IsActive();
        }
    }


    public static void Hide()
    {
        _instance.canvas.enabled = false;
    }

    private void OnDestroy()
    {
        _instance = null;
    }


    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
            canvas = GetComponent<Canvas>();
            keyHint.gameObject.SetActive(false);
            canvas.enabled = false;
        } else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        interactHint.rectTransform.Rotate(interactHint.rectTransform.forward, rotationSpeed * Time.deltaTime);
    }
}
