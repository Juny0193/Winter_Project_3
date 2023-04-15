using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeButtonImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    // ��ư ������Ʈ�� ��ư �̹��� ������Ʈ�� ������ ����
    private Button button;
    private Image buttonImage;

    // �� ���¿� �ش��ϴ� �̹����� ������ ����
    public Sprite normalStateImage;
    public Sprite hoverStateImage;
    public Sprite pressedStateImage;

    void Start()
    {
        // ��ư ������Ʈ�� ��ư �̹��� ������Ʈ�� ������
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        // ��ư�� �⺻ �̹����� ������
        buttonImage.sprite = normalStateImage;
    }

    // ���콺 �����Ͱ� ��ư ���� �ö��� �� ȣ���
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ��ư �̹����� hover ������ �̹����� ������
        if (button.interactable && buttonImage.sprite == normalStateImage)
        {
            buttonImage.sprite = hoverStateImage;
        }
    }

    // ���콺 �����Ͱ� ��ư���� ����� �� ȣ���
    public void OnPointerExit(PointerEventData eventData)
    {
        // ��ư �̹����� normal ������ �̹����� ������
        if (button.interactable && buttonImage.sprite == hoverStateImage)
        {
            buttonImage.sprite = normalStateImage;
        }
    }

    // ��ư�� ������ �� ȣ���
    public void OnPointerDown(PointerEventData eventData)
    {
        // ��ư �̹����� pressed ������ �̹����� ������
        if (button.interactable && (buttonImage.sprite == hoverStateImage || buttonImage.sprite == normalStateImage))
        {
            buttonImage.sprite = pressedStateImage;
        }
    }

    // ��ư���� ���콺 ��ư�� ������ �� ȣ���
    public void OnPointerUp(PointerEventData eventData)
    {
        // ��ư �̹����� normal ���� �Ǵ� hover ������ �̹����� ������
        if (button.interactable && buttonImage.sprite == pressedStateImage)
        {
            buttonImage.sprite = normalStateImage;
        }
    }
}