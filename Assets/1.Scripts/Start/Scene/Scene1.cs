using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Experimental.Rendering.Universal;


using static Yields;
using static Define;


public class Scene1 : MonoBehaviour, IChangeable
{
    [SerializeField]
    private GameObject _SoundObj;


    [SerializeField]
    private Image _fadeObject;
    [SerializeField]
    private TMP_Text _title;
    [SerializeField]
    private TMP_Text _subtitle;
    [SerializeField]
    private Light2D _titleLight;
    [SerializeField]
    private Light2D _subtitleLight;
    [SerializeField]
    private Color _tmpColor;
    [SerializeField]
    private Color _fadeColor;
    [SerializeField]
    private Color _firstFadeColor;
    [SerializeField]
    private float _offLightIntencity = 0.9f;
    [SerializeField]
    private float _cameraPos = 38f;


    private Color _titleColor;
    private Color _subtitleColor;
    private Color _titleLightColor;
    private Color _subtitleLightColor;
    private float _lightIntencity = 1.2f;

    private void Start()
    {
        _titleColor = _title.color;
        _subtitleColor = _subtitle.color;
        _titleLightColor = _titleLight.color;
        _subtitleLightColor = _subtitleLight.color;
        _lightIntencity = _titleLight.intensity;
        OffLight();
        _fadeObject.color = _firstFadeColor;
        _SoundObj.SetActive(true);
        StartCoroutine(LightFlicker());
    }

    [ContextMenu("색깔 변환")]
    public void SceneChange()
    {
        StartCoroutine(LightFlicker());
    }

    [ContextMenu("끄기")]
    private void OffLight()
    {

        _title.color = _tmpColor;
        _subtitle.color = _tmpColor;
        _fadeObject.color = _fadeColor;

        _titleLight.color = _tmpColor;
        _subtitleLight.color = _tmpColor;
        _titleLight.intensity = _offLightIntencity;
        _subtitleLight.intensity = _offLightIntencity;
    }

    [ContextMenu("켜기")]
    private void OnLight()
    {
        _title.color = _titleColor;
        _subtitle.color = _subtitleColor;
        _fadeObject.color = Vector4.zero;

        _titleLight.color = _titleLightColor;
        _subtitleLight.color = _subtitleLightColor;
        _titleLight.intensity = _lightIntencity;
        _subtitleLight.intensity = _lightIntencity;
    }
    private IEnumerator LightFlicker()
    {
        #region Neon1
        // yield return WaitForSeconds(1.43f); //1.43
        // OnLight();
        // yield return WaitForSeconds(0.3f); //1.73
        // OffLight();
        // yield return WaitForSeconds(0.1f); // 1.83
        // OnLight();
        // yield return WaitForSeconds(0.2f); // 2.03
        // OffLight();
        // yield return WaitForSeconds(0.4f); // 2.43
        // OnLight();
        // yield return WaitForSeconds(0.3f); // 2.73
        // OffLight();
        // yield return WaitForSeconds(0.6f); // 2.73
        // OnLight();
        #endregion
        float[] neonDuration = { 1.4f, 0.2f, 0.1f, 0.2f, 0.4f, 0.2f, 0.7f, 0.3f, 0.04f, 0.06f, 0.14f, 0.06f, 0.5f };
        bool isOn = false;
        for (int i = 0; i < neonDuration.Length; i++)
        {
            yield return WaitForSeconds(neonDuration[i]);
            if (isOn)
            {
                OffLight();
            }
            else
            {
                OnLight();
            }
            isOn = !isOn;
        }

        yield return WaitForSeconds(1f);
        MaincamTransform.DOMoveX(_cameraPos, 0.5f).OnComplete(() =>
        {
            FlickerDirect.Instance.SceneChange(2);
        });
    }
}
