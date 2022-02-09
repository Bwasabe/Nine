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



    private void Awake()
    {
        GameDialogVO textData = null;
        Addressables.LoadAssetAsync<TextAsset>(dialogPath).Completed += (AsyncOperationHandle<TextAsset> dialog) =>
        {
            textData = JsonUtility.FromJson<GameDialogVO>(dialog.Result.ToString());
        };

        foreach (DialogVO vo in textData.list)
        {
            dialogTextDictionary.Add(vo.code, vo.text);
        }

        Param2EventManager<int, Action>.StartListening("SHOW_DIALOG", ShowDialog);
    }


    public void ShowDialog(int index, Action callback = null)
    {
        if (index >= dialogTextDictionary.Count)
        {
            return;
        }

        dialogPanel.StartDialog(dialogTextDictionary[index], callback);
    }
}
