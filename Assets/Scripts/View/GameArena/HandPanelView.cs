﻿using UnityEngine.EventSystems;
using View.DeckItems;

namespace View.GameArena
{
    public class HandPanelView : DroppableView
    {        

        /// <summary>
        /// On Drop dragable View
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnDrop(PointerEventData eventData)
        {
            var draggableCard = eventData.pointerDrag.GetComponent<DraggableView>();
            if (draggableCard != null && draggableCard.IsDroppable && draggableCard.PlaceholderParent == transform)
            {
                draggableCard.ParentToReturnTo = transform;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// On poiter enter
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;
            var draggableCard = eventData.pointerDrag.GetComponent<DraggableView>();

            if (draggableCard as CardView)
            {
                if (!draggableCard.IsDroppable) return;
                base.OnPointerEnter(eventData);
            }
            else if (draggableCard as TrateView)
            {
                base.OnPointerEnter(eventData);
            }
        }
    }
}