using static Yields;

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;
using System.Reflection;

public class Scene1 : MonoBehaviour, IChangeable
{
    [SerializeField]
    private GameObject _soundObj;


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
    private float _offLightIntencity = 0.9f;


    private Color _titleColor;
    private Color _subtitleColor;
    private Color _titleLightColor;
    private Color _subtitleLightColor;
    private float _lightIntencity = 1.2f;

    private Transform _thisTransform = null;

    private Coroutine _lightCoroutine = null;

    private void Awake()
    {
        EventManager.StartListening("Scene1" , Scene1ToScene2);
    }
    private void Start()
    {
        _thisTransform = transform;
        _titleColor = _title.color;
        _subtitleColor = _subtitle.color;
        _titleLightColor = _titleLight.color;
        _subtitleLightColor = _subtitleLight.color;
        _lightIntencity = _titleLight.intensity;
        ManagerStart.Instance.FirstFade();
        _soundObj.SetActive(true);
        OffLight();
    }

    [ContextMenu("색깔 변환")]
    public void SceneChange()
    {
        _lightCoroutine = StartCoroutine(LightFlicker());
    }

    [ContextMenu("끄기")]
    private void OffLight()
    {

        _title.color = _tmpColor;
        _subtitle.color = _tmpColor;
        ManagerStart.Instance.Fade();

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
        ManagerStart.Instance.FadeObject.color = Vector4.zero;

        _titleLight.color = _titleLightColor;
        _subtitleLight.color = _subtitleLightColor;
        _titleLight.intensity = _lightIntencity;
        _subtitleLight.intensity = _lightIntencity;
    }


    private void Scene1ToScene2()
    {
        FlickerDirect.Instance.SceneChange(2);
        StopCoroutine(_lightCoroutine);
        _soundObj.SetActive(false);
        ManagerStart.Instance.SetCurrentScene(MethodBase.GetCurrentMethod().DeclaringType.FullName, 2);
    }


    private IEnumerator LightFlicker()
    {
        yield return WaitUntil(() => _soundObj != null);

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
        float[] neonDuration = { 1.3f, 0.2f, 0.1f, 0.2f, 0.4f, 0.2f, 0.7f, 0.3f, 0.04f, 0.06f, 0.14f, 0.06f, 0.5f };
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
        // FlickerDirect.Instance.SceneChange(2);
        FlickerDirect.Instance.SceneChange(2);
        ManagerStart.Instance.SetCurrentScene(MethodBase.GetCurrentMethod().DeclaringType.FullName, 2);


    }
}
