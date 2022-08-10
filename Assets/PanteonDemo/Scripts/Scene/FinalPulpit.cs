using UnityEngine;
using DG.Tweening;

public class FinalPulpit : MonoBehaviour
{
    private void OnEnable()
    {
        PulpitRight();
    }

    private void PulpitRight()
    {
        transform.DOLocalMoveX(0, 1f);
    }
}