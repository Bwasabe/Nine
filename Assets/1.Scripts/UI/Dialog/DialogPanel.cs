using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DialogPanel : MonoBehaviour
{
    private List<TextVO> list;
    private Dictionary<int, Sprite> imageDictionary = new Dictionary<int, Sprite>();
    private Action endDialogCallback = null;


    [SerializeField]
    private TMP_Text dialogText;


    [SerializeField]
    private GameObject nextIcon;
    [SerializeField]
    private GameObject typeEffectParticle;

    [SerializeField]
    private Image profileImage;

    [SerializeField]
    private float typeDuration = 0.1f;

    [SerializeField]
    private AudioClip typeClip;


    private RectTransform textTransform;
    private RectTransform panel;

    private int currentIndex;

    private bool clickToNext = false;
    private bool isOpen = false;


    private void Awake()
    {
        panel = GetComponent<RectTransform>();
        textTransform = dialogText.GetComponent<RectTransform>();
    }

    public void StartDialog(List<TextVO> list, Action callback = null)
    {
        endDialogCallback = callback;
        this.list = list;
        ShowDialog();
    }

    private void ShowDialog()
    {
        currentIndex = 0;
        GameManager.Instance.TimeScale = 0f;

        profileImage.sprite = null;
        dialogText.text = "";

        panel.DOScale(new Vector3(1, 1, 1), 0.8f).OnComplete(() =>
        {
            TypeIt(list[currentIndex]);
            isOpen = true;
        });
    }

    private void TypeIt(TextVO vo)
    {
        int idx = vo.icon;
        //????? ????????? ??????? ????? ??????? ?????? ?????? ??.

        if (!imageDictionary.ContainsKey(idx))
        {
            Sprite img = Resources.Load<Sprite>($"profile{idx}");
            imageDictionary.Add(idx, img);
        }

        profileImage.sprite = imageDictionary[idx];

        dialogText.text = vo.msg;
        nextIcon.SetActive(false);
        clickToNext = false;
        StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        dialogText.ForceMeshUpdate();
        dialogText.maxVisibleCharacters = 0;
        int totalVisibleChar = dialogText.textInfo.characterCount;
        for (int i = 1; i <= totalVisibleChar; i++)
        {
            dialogText.maxVisibleCharacters = i;

            //TODO: 파티클 재생
            //Vector3 pos = dialogText.textInfo.characterInfo[i - 1].bottomRight;
            //Vector3 tPos = textTransform.TransformPoint(pos);

            //TODO: 사운드 재생

            if (clickToNext)
            {
                dialogText.maxVisibleCharacters = totalVisibleChar;
                break;
            }
            yield return Yields.WaitForSecondsRealtime(typeDuration);
        }

        currentIndex++;
        clickToNext = true;
        nextIcon.SetActive(true);
    }

    private void Update()
    {
        if (!isOpen) return;

        if (Input.GetButtonDown("Jump"))
        {
            if (clickToNext)
            {
                if (currentIndex >= list.Count)
                {
                    panel.DOScale(new Vector3(0, 0, 1), 0.8f).OnComplete(() =>
                    {

                        GameManager.Instance.TimeScale = 1f;
                        isOpen = false;
                        if (endDialogCallback != null)
                        {
                            endDialogCallback();
                        }
                    });
                }
                else
                {
                    TypeIt(list[currentIndex]);
                }
            }
            else
            {
                clickToNext = true;
            }
        }
    }


}
