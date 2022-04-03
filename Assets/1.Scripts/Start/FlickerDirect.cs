using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerDirect : MonoSingleton<FlickerDirect>
{
    private string _sceneName = "Scene";

    public void SceneChange(int nextIndex)
    {
        Transform scene = transform.Find($"{_sceneName}{nextIndex}");
        scene.GetComponent<IChangeable>().SceneChange();
    }
    private void Start() {
        SceneChange(1);
    }

}
