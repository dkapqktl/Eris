using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemSlotInfo : UIBase
{
    [SerializeField] Image iconImage;
    [SerializeField] TextMeshProUGUI amountText;

    [SerializeField] Sprite noneIcon; // Sprite(스프라이트) = 게임 화면에 출력하기위한 가공된 이미지, Texture(텍스처) = 원본 이미지 데이터

    ItemSlot connectedSlot; // 일단 저장해두기

    public void ConnectSlot(ItemSlot targetSlot)
    {
        if (targetSlot is null) return;
        connectedSlot = targetSlot; // 타겟슬롯이 있다면 일단 저장하기

        VisualUpdate(connectedSlot);
    }

    protected virtual void VisualUpdate(ItemSlot targetSlot)
    {
        if (connectedSlot is null) return;

        ItemContainer targetItem = targetSlot.GetItem();

        if (iconImage)
        {
            if (targetItem)
            {
                iconImage.sprite = targetItem.icon ?? noneIcon; // 타겟아이템에 아이콘이 없으면 논아이콘을 표시
                iconImage.enabled = true; // 아이템이 없을때 아이콘 활성화
            }
            else
            {
                iconImage.enabled = false; // 아이템이 없을때 아이콘 비활성화
            }
        }

        if (amountText)
        {
            int targetStack = targetSlot.GetStack();

            if (!targetItem || targetItem.maxStack <= 1 || targetStack <= 0) // 아이템이 없거나 스택을 쌓지안는 아이템이이라면, 또는 버그걸려서 아이템이 0이거나 음수라면
            {
                amountText.SetText("");
            }
            else 
            {
                bool isMax = targetSlot.GetIsMax(); // 아이템 스텍이 꽉찻을때 트루 // 이걸 통해 멕스일때 어떻게 텍스트출력할지 등 설정 할 수 있음
                amountText.color = Color.yellow; 
                amountText.SetText($"{targetStack}");
            }
        }
    }
}
