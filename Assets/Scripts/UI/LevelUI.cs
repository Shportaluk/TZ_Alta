using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public event Action onClickRestart;

    [SerializeField] private AbstractPanel _pnlGameOver;
    [SerializeField] private AbstractPanel _pnlLevelCompleted;
    [SerializeField] private Button _btnRestart;
    [SerializeField] private float _delayToShowRestartButton = 1f;

    private void Awake()
    {
        _btnRestart.onClick.AddListener(() => onClickRestart?.Invoke());
    }

    public void Show(LevelEndStatus levelEndStatus)
    {
        switch(levelEndStatus)
        {
            case LevelEndStatus.GameOver:
                _pnlGameOver.Show();
                _pnlLevelCompleted.Hide();
                break;

            case LevelEndStatus.LevelCompleted:
                _pnlGameOver.Hide();
                _pnlLevelCompleted.Show();
                break;
        }

        this.DoAction(_delayToShowRestartButton, () => _btnRestart.gameObject.SetActive(true));
    }
}

public enum LevelEndStatus
{
    GameOver = 0,
    LevelCompleted = 1,
}