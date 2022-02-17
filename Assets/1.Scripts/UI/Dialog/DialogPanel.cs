using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
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
    public Dictionary<string, Action> middleDialogCallBack { get; private set; } = new Dictionary<string, Action>();


    [SerializeField]
    private TMP_Text dialogText;
    [SerializeField]
    private Text nameText;


    // [SerializeField]
    // private GameObject nextIcon;
    [SerializeField]
    private GameObject typeEffectParticle;


    [SerializeField]
    private Image[] profileImages;
    [SerializeField]
    private float blackImageColor;


    [SerializeField]
    private AudioClip typeClip;


    [SerializeField]
    private float typeDuration = 0.1f;

    [SerializeField]
    private string spriteAtlasPath = "SpriteFaces";
    [SerializeField]
    private string[] spriteFaceNames;

    private SpriteAtlas spritefaces;

    private RectTransform textTransform;
    private RectTransform panel;

    private AsyncOperationHandle handle;

    private int currentIndex;
    private int currentIcon = 0;


    private bool clickToNext = false;
    private bool isOpen = false;


    private void Awake()
    {
        panel = GetComponent<RectTransform>();
        textTransform = dialogText.GetComponent<RectTransform>();
        Addressables.LoadAssetAsync<SpriteAtlas>(spriteAtlasPath).Completed += (obj) =>
        {
            handle = obj;
            spritefaces = obj.Result;
        };
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        yield return Yields.WaitUntil(() => spritefaces != null);
        Addressables.Release(handle);
        for (int i = 0; i < spritefaces.spriteCount; i++)
        {
            imageDictionary.Add(i, spritefaces.GetSprite(spriteFaceNames[i]));
            Debug.Log(imageDictionary[i]);
        }
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

        currentIcon = list[currentIndex].icon;


        GameManager.Instance.TimeScale = 0f;

        for (int i = 0; i < profileImages.Length; i++)
        {
            profileImages[i].enabled = true;
        }
        dialogText.text = "";

        panel.DOScale(new Vector3(1, 1, 1), 0.8f).OnComplete(() =>
        {
            profileImages[(int)list[currentIndex].facePosition].sprite = imageDictionary[list[currentIndex].icon];
            profileImages[(int)list[currentIndex].facePosition].DOFade(1f, 0.5f);
            profileImages[(int)list[currentIndex].facePosition].DOColor(Color.white, 0.5f).OnComplete(() =>
            {
                TypeIt(list[currentIndex]);
                isOpen = true;
            });
        });
    }

    private void TypeIt(TextVO vo)
    {
        Debug.Log(vo);
        Debug.Log(vo.facePosition);
        profileImages[(int)list[currentIndex].facePosition].sprite = imageDictionary[list[currentIndex].icon];
        if (currentIcon != vo.icon)
        {
            profileImages[(int)list[currentIndex - 1].facePosition].DOColor(new Color(blackImageColor, blackImageColor, blackImageColor, 1f), 0.3f);
            profileImages[(int)list[currentIndex - 1].facePosition].GetComponent<RectTransform>().DOSizeDelta(Vector2.one * 800f, 0.3f);
            profileImages[(int)vo.facePosition].DOColor(Color.white, 0.3f);
            profileImages[(int)vo.facePosition].DOFade(1f, 0.5f);
            profileImages[(int)vo.facePosition].GetComponent<RectTransform>().DOSizeDelta(Vector2.one * 1000f, 0.3f);

        }
        int idx = vo.icon;
        currentIcon = idx;

        nameText.text = string.Format(vo.name);
        dialogText.text = vo.msg;

        //nextIcon.SetActive(false);
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
            //생각좀 해봐야할듯: 중간 콜백 만들기(Json에 중간에 string형 문자를 만들어 null이 아닐경우 dictionary에서 빼서 사용하게 만들기)

            yield return Yields.WaitForSecondsRealtime(typeDuration);
        }

        currentIndex++;
        clickToNext = true;
        //nextIcon.SetActive(true);
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
                    for (int i = 0; i < profileImages.Length; i++)
                    {
                        profileImages[i].color = new Color(blackImageColor, blackImageColor, blackImageColor, 0f);
                        profileImages[i].enabled = false;

                    }
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
