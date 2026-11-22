using Zenject;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [Inject] private Controls.GameActions _gameActions;

    [Header("UI")]
    [SerializeField] private Image _fillBar;

    private float _fillProgress = 0f;
    private float _fillSpeed = 2f;   // скорость опадания (доли шкалы в секунду)
    private float _holdSpeed = 0.5f; // секунд удержания до полной шкалы

    private void OnEnable()
    {
        _gameActions.Enable();
        _gameActions.Restart.canceled += OnRestartCanceled;
    }

    private void OnDisable()
    {
        _gameActions.Restart.canceled -= OnRestartCanceled;
        _gameActions.Disable();
    }

    private void Update()
    {
        bool pressed = _gameActions.Restart.IsPressed();

        if (pressed)
        {
            _fillProgress = Mathf.Min(1f, _fillProgress + Time.deltaTime / _holdSpeed);
            UpdateFillBar();
        }
        else if (_fillProgress > 0f)
        {
            _fillProgress = Mathf.Max(0f, _fillProgress - Time.deltaTime * _fillSpeed);
            UpdateFillBar();
        }
    }

    private void OnRestartCanceled(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        if (_fillProgress >= 1f)
        {
            RestartGame();
        }
        else
        {
            _fillProgress = 0f;
            UpdateFillBar();
        }
    }

    private void UpdateFillBar()
    {
        if (_fillBar == null) return;

        RectTransform panel = _fillBar.transform.parent as RectTransform;
        float maxWidth = panel.rect.width - 16f; // 8 слева + 8 справа
        _fillBar.rectTransform.sizeDelta = new Vector2(_fillProgress * maxWidth, _fillBar.rectTransform.sizeDelta.y);
    }

    private void RestartGame()
    {
        Debug.Log("[InputManager] Перезагрузка сцены!");
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
