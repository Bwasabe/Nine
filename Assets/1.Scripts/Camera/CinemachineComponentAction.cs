using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineComponentAction : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _cinemachineCam = null;


    public enum CinemachineComponent
    {
        Third_Person_Follow,
        Framing_Transposer,
        Hard_Look_To_Target,
        Orbital_Transposer,
        Tracked_Dolly,
        Transposer,
    }

    [SerializeField]
    private CinemachineComponent _cinemachineComponent;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))return;
        switch (_cinemachineComponent)
        {
            case CinemachineComponent.Third_Person_Follow:
                _cinemachineCam.AddCinemachineComponent<Cinemachine3rdPersonFollow>();
                break;

            case CinemachineComponent.Framing_Transposer:
                _cinemachineCam.AddCinemachineComponent<CinemachineFramingTransposer>();
                break;

            case CinemachineComponent.Hard_Look_To_Target:
                _cinemachineCam.AddCinemachineComponent<CinemachineHardLockToTarget>();
                break;

            case CinemachineComponent.Orbital_Transposer:
                _cinemachineCam.AddCinemachineComponent<CinemachineOrbitalTransposer>();
                break;

            case CinemachineComponent.Tracked_Dolly:
                _cinemachineCam.AddCinemachineComponent<CinemachineTrackedDolly>();
                break;

            case CinemachineComponent.Transposer:
                _cinemachineCam.AddCinemachineComponent<CinemachineTransposer>();
                break;

            default:
                break;
        }
    }


}
