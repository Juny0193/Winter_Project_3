using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeButtonImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    // 버튼 컴포넌트와 버튼 이미지 오브젝트를 저장할 변수
    private Button button;
    private Image buttonImage;

    // 각 상태에 해당하는 이미지를 저장할 변수
    public Sprite normalStateImage;
    public Sprite hoverStateImage;
    public Sprite pressedStateImage;

    void Start()
    {
        // 버튼 컴포넌트와 버튼 이미지 오브젝트를 가져옴
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        // 버튼의 기본 이미지를 설정함
        buttonImage.sprite = normalStateImage;
    }

    // 마우스 포인터가 버튼 위에 올라갔을 때 호출됨
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 버튼 이미지를 hover 상태의 이미지로 변경함
        if (button.interactable && buttonImage.sprite == normalStateImage)
        {
            buttonImage.sprite = hoverStateImage;
        }
    }

    // 마우스 포인터가 버튼에서 벗어났을 때 호출됨
    public void OnPointerExit(PointerEventData eventData)
    {
        // 버튼 이미지를 normal 상태의 이미지로 변경함
        if (button.interactable && buttonImage.sprite == hoverStateImage)
        {
            buttonImage.sprite = normalStateImage;
        }
    }

    // 버튼이 눌렸을 때 호출됨
    public void OnPointerDown(PointerEventData eventData)
    {
        // 버튼 이미지를 pressed 상태의 이미지로 변경함
        if (button.interactable && (buttonImage.sprite == hoverStateImage || buttonImage.sprite == normalStateImage))
        {
            buttonImage.sprite = pressedStateImage;
        }
    }

    // 버튼에서 마우스 버튼을 떼었을 때 호출됨
    public void OnPointerUp(PointerEventData eventData)
    {
        // 버튼 이미지를 normal 상태 또는 hover 상태의 이미지로 변경함
        if (button.interactable && buttonImage.sprite == pressedStateImage)
        {
            buttonImage.sprite = normalStateImage;
        }
    }
}