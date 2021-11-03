using Input;
using UnityEngine;

namespace Scripts
{
    public class MouseToGrid : MonoBehaviour
    {
        [SerializeField] private InputController _inputController;
        [SerializeField] private ArtCollection _artToPlace = null;

        private Camera _mainCamera;
        private Ray _mouseRay;

        private void Awake()
        {
            if (_inputController == null) {
                _inputController = FindObjectOfType<InputController>();
            }
            _mainCamera = Camera.main;
            _artToPlace.Verify();
        }

        private void OnEnable()
        {
            _inputController.MouseMoved += OnMouseMoved;
            _inputController.LeftClick += OnLeftClick;
        }

        private void OnDisable()
        {
            _inputController.MouseMoved -= OnMouseMoved;
            _inputController.LeftClick -= OnLeftClick;
        }

        private void Update()
        {
            Physics.Raycast(_mouseRay, out var hit, 100f);
            if (hit.collider == null) {
                HoverSelectedController.instance.DisableHover();
                return;
            }

            GridSlot slot = hit.collider.GetComponent<GridSlot>();
            if (slot != null) {
                slot.OnHover();
            }
        }

        private void OnMouseMoved(Vector2 mousePos)
        {
            _mouseRay = _mainCamera.ScreenPointToRay(mousePos);
        }

        private void OnLeftClick(Vector2 mousePos)
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            Physics.Raycast(ray, out var hit, 100f);
            if (hit.collider == null) {
                HoverSelectedController.instance.DisableSelected();
                return;
            }

            GridSlot slot = hit.collider.GetComponent<GridSlot>();
            if (slot != null) {
                GameObject objToPlace = _artToPlace.GetArt();
                slot.OnSelect(objToPlace);
            }
        }
    }
}