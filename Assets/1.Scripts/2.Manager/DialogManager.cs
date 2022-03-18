using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

public class DialogManager : MonoBehaviour
{
    private Dictionary<int, List<TextVO>> dialogTextDictionary = new Dictionary<int, List<TextVO>>();


    [SerializeField]
    private DialogPanel dialogPanel;

    [SerializeField]
    private string dialogPath = "대화 내용";

    private AsyncOperationHandle handle;

    private Action<EventParam> _showDialog;

    private void Awake()
    {

        GameTextDataVO textData = null;
        Addressables.LoadAssetAsync<TextAsset>(dialogPath).Completed += (AsyncOperationHandle<TextAsset> obj) =>
        {
            handle = obj;
            textData = JsonUtility.FromJson<GameTextDataVO>(obj.Result.ToString());
            Debug.Log(obj.Result.ToString());
            StartCoroutine(Init(textData));
        };
    }
    private IEnumerator Init(GameTextDataVO textData)
    {
        yield return Yields.WaitUntil(() => textData != null);
        foreach (DialogVO vo in textData.list)
        {
            dialogTextDictionary.Add(vo.code, vo.text);
        }
        Addressables.Release(handle);

        EventParam eventParam = new EventParam {objs = new object[] {1 , null} };
        _showDialog = (eventParam) => {
            ShowDialog((int)eventParam.objs[0] , (Action)eventParam.objs[1]);
        };
        ParamEventManager.StartListening("SHOW_DIALOG", _showDialog);
        ShowDialog(0);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Alpha0)){
            ShowDialog(1);
        }
    }

    private void ShowDialog(int index, Action callback = null)
    {
        if (index > dialogTextDictionary.Count)
        {
            return;
        }

        dialogPanel.StartDialog(dialogTextDictionary[index], callback);
    }
}
