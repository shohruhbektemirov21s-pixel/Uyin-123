using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace BlockBlast.UI {
    [RequireComponent(typeof(Button))]
    public class AnimatedButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
        private Vector3 _originalScale;
        public Action OnClick;
        private void Awake() {
            _originalScale = transform.localScale;
            GetComponent<Button>().onClick.AddListener(() => OnClick?.Invoke());
        }
        public void OnPointerDown(PointerEventData e) => transform.localScale = _originalScale * 0.9f;
        public void OnPointerUp(PointerEventData e) => transform.localScale = _originalScale;
    }
}
