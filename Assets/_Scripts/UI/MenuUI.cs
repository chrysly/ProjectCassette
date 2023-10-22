using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour {
    [SerializeField] private Transform spider;
    private Vector3 spiderPos;
    private Vector3 oldSpiderPos;

    [SerializeField] private Transform mainText;
    private Vector3 mainTextPos;
    private Vector3 oldMainTextPos;

    [SerializeField] private TMP_InputField textField;
    private Vector3 textFieldPos;
    private Vector3 oldTextFieldPos;
    
    [SerializeField] private Transform enterText;
    private Vector3 enterTextPos;
    private Vector3 oldEnterTextPos;

    private bool inMainMenu = true;

    private string username;

    [SerializeField] private List<CanvasGroup> textList;
    
    // Start is called before the first frame update
    void Awake() {
        spiderPos = spider.position;
        mainTextPos = mainText.position;
        textFieldPos = textField.transform.position;
        enterTextPos = enterText.position;
        Shift();
    }

    private void Shift() {
        oldSpiderPos = new Vector3(spiderPos.x - 700, spiderPos.y, spiderPos.z);
        oldMainTextPos = new Vector3(mainTextPos.x, mainTextPos.y + 300, mainTextPos.z);
        oldTextFieldPos = new Vector3(textFieldPos.x, textFieldPos.y - 450, textFieldPos.z);
        oldEnterTextPos = new Vector3(enterTextPos.x, enterTextPos.y - 400, enterTextPos.z);

        spider.position = oldSpiderPos;
        mainText.position = oldMainTextPos;
        textField.transform.position = oldTextFieldPos;
        enterText.position = oldEnterTextPos;

        foreach (CanvasGroup text in textList) {
            text.DOFade(0f, 0f);
        }
    }

    private void Start() {
        StartCoroutine(StartAction());
    }

    private IEnumerator StartAction() {
        spider.DOMove(spiderPos, 2f).SetEase(Ease.OutFlash);
        yield return new WaitForSeconds(1.2f);
        mainText.DOMove(mainTextPos, 1f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(0.5f);
        textField.transform.DOMove(textFieldPos, 1f).SetEase(Ease.OutFlash);
        yield return new WaitForSeconds(0.2f);
        enterText.DOMove(enterTextPos, 1f).SetEase(Ease.OutFlash);
        yield return null;
    }

    private IEnumerator HideAction() {
        spider.DOMove(oldSpiderPos, 1f).SetEase(Ease.OutFlash);
        mainText.DOMove(oldMainTextPos, 1f).SetEase(Ease.OutFlash);
        textField.transform.DOMove(oldTextFieldPos, 1f).SetEase(Ease.OutFlash);
        enterText.DOMove(oldEnterTextPos, 1f).SetEase(Ease.OutFlash);
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (inMainMenu) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                if (textField.text != null) {
                    inMainMenu = false;
                    username = textField.text;
                    textField.text = null;
                    StartCoroutine(HideAction());
                    StartCoroutine(ControlsDisplay());
                }
            }
        }
    }

    private IEnumerator ControlsDisplay() {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < textList.Count; i++) {
            textList[i].DOFade(1f, 1f);
            StartCoroutine(FadeOut(textList[i]));
            yield return new WaitForSeconds(2f);
        }
    }

    private IEnumerator FadeOut(CanvasGroup text) {
        yield return new WaitForSeconds(4f);
        text.DOFade(0f, 1f);
    }
}
