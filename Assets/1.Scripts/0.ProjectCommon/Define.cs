using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public static class Define
{
    private static Camera _mainCam;

    public static Camera MainCam{
        get {
            if (_mainCam == null)
            {
                _mainCam = Camera.main;
            }
            return _mainCam;
        }
    }

    public static Transform MaincamTransform{
        get{
            if(_mainCamTransform == null){
                _mainCamTransform = MainCam.transform;
            }
            return _mainCamTransform;
        }
    }
    private static Transform _mainCamTransform;

    public static Image FadeObject{
        get{
            if(_fadeObject == null){
                _fadeObject = ObjectManager.Instance.FadeObject;
            }
            return _fadeObject;
        }
    }

    private static Image _fadeObject = null;

    public static CinemachineVirtualCamera VirtualCamera{
        get{
            if(_vcam == null){
                _vcam = ObjectManager.Instance.VirtualCamera;
            }
            return _vcam;
        }
    }

    private static CinemachineVirtualCamera _vcam;

}
