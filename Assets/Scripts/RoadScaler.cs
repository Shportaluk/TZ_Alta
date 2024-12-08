using UnityEngine;

public class RoadScaler : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _scaleMultyplier = 0.1f;

    private void Awake()
    {
        _player.onScale += OnScalePlayer;
    }

    private void OnScalePlayer()
    {
        var scale = transform.localScale;
        scale.x = _player.transform.localScale.x * _scaleMultyplier;
        transform.localScale = scale;
    }
}