using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FilmGrainEffect : MonoBehaviour
{
    public Volume volume;

    public Vector2 range;

    private bool increase = true;

    private void FixedUpdate()
    {
        if (volume.profile.TryGet<FilmGrain>(out var filmGrain))
        {
            if (filmGrain.intensity.value >= range.y)
            {
                increase = false;
            }

            if (filmGrain.intensity.value <= range.x)
            {
                increase = true;
            }

            filmGrain.intensity  = new ClampedFloatParameter(filmGrain.intensity.value + (increase ? 0.0005f : -0.0005f), 0, 1f);
        }
    }
}