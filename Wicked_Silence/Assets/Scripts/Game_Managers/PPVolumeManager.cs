using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;
public class PPVolumeManager : MonoBehaviour {
    [Header("Post Processing Hiding Volume Settings")]
    [SerializeField] private Volume volume;
    [SerializeField] private PlayerDetectObjects player;
    [SerializeField] private float hidingVignetteIntensity;
    [SerializeField] private float hidingContrastIntensity;
    [SerializeField] private float transitionTime;
    private Vignette vignette;
    private ColorAdjustments colorAdjustments;
    private float originalVignetteIntensity;
    private float originalContrastIntensity;
    bool returnedToOriginalValues = false;
    // Start is called before the first frame update
    void Start() {
        Init();
    }
    // Update is called once per frame
    void Update() {
        AdjustHidingPPEffect();
    }
    //Initializing the pp effects...
    void Init() {
        if (volume.profile.TryGet(out vignette) && volume.profile.TryGet(out colorAdjustments)) {
            originalVignetteIntensity = vignette.intensity.value;
            originalContrastIntensity = colorAdjustments.contrast.value;
        }
    }
    //If player is hiding in a closed Hideable Object type we increase the vignette & the contrast...
    void AdjustHidingPPEffect() {
        if (volume.profile.TryGet(out vignette) && volume.profile.TryGet(out colorAdjustments)) {
            returnedToOriginalValues = vignette.intensity.value.Equals(originalVignetteIntensity) && colorAdjustments.contrast.value.Equals(originalContrastIntensity);
            if (player.hiding && (player.GetObjectType().Equals("Closed"))) {
                vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, hidingVignetteIntensity, transitionTime * Time.deltaTime);
                colorAdjustments.contrast.value = Mathf.Lerp(colorAdjustments.contrast.value, hidingContrastIntensity, transitionTime * Time.fixedDeltaTime);
            }
            else {
                //FIX THIS SO IT WON'T RUN FOREVER WHEN WE ARE NOT HIDING...
                if (!returnedToOriginalValues) {
                    //REMINDER: MAKE THIS SO THAT IT ONLY RUNS WHEN NEEDED & NOT ON EVERY FRAME...
                    vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, originalVignetteIntensity, transitionTime * Time.deltaTime);
                    colorAdjustments.contrast.value = Mathf.Lerp(colorAdjustments.contrast.value, originalContrastIntensity, transitionTime * Time.fixedDeltaTime);
                    if (Mathf.Approximately(vignette.intensity.value, originalContrastIntensity) && Mathf.Approximately(colorAdjustments.contrast.value, originalContrastIntensity)) {
                        return;
                    }
                }
                else {
                    return;
                }
            }
        
        }
    }
}