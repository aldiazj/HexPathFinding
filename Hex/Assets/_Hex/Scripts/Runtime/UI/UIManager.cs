using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Hex.Runtime.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Toggle cellStartToggle;
        [SerializeField] private Toggle cellEndToggle;
        [SerializeField] private Toggle cellObstacleToggle;
        [SerializeField] private Button findPathButton;
        [SerializeField] private Button clearButton;
        [SerializeField] private TextMeshProUGUI pathLengthMesh;
        [SerializeField] private TextMeshProUGUI cellStartCoordinatesTextMesh;
        [SerializeField] private TextMeshProUGUI cellEndCoordinatesTextMesh;

        public void Init(Action onStartCellPressed, Action onEndCellPressed,Action onObstacleCellPressed, UnityAction onFindPathPressed, UnityAction onClearPressed)
        {
            cellStartToggle.onValueChanged.AddListener(isOn =>
            {
                if (isOn)
                {
                    onStartCellPressed?.Invoke();
                }
            });
            cellEndToggle.onValueChanged.AddListener(isOn =>
            {
                if (isOn)
                {
                    onEndCellPressed?.Invoke();
                }
            });
            cellObstacleToggle.onValueChanged.AddListener(isOn =>
            {
                if (isOn)
                {
                    onObstacleCellPressed?.Invoke();
                }
            });
            findPathButton.onClick.AddListener(onFindPathPressed);
            clearButton.onClick.AddListener(onClearPressed);
        }

        private void OnDestroy()
        {
            cellStartToggle.onValueChanged.RemoveAllListeners();
            cellEndToggle.onValueChanged.RemoveAllListeners();
            cellObstacleToggle.onValueChanged.RemoveAllListeners();
            findPathButton.onClick.RemoveAllListeners();
            clearButton.onClick.RemoveAllListeners();
        }

        public void ShowResult(int pathLength)
        {
            pathLengthMesh.text = $"Path length: {pathLength}";
        }

        public void ClearResult()
        {
            pathLengthMesh.text = "Path length:";
            cellStartCoordinatesTextMesh.text = "";
            cellEndCoordinatesTextMesh.text = "";
        }

        public void SetFindAvailability(bool isFindButtonAvailable)
        {
            findPathButton.interactable = isFindButtonAvailable;
        }

        public void SetStartCellCoordinates(string coordinates)
        {
            cellStartCoordinatesTextMesh.text = coordinates;
        }

        public void SetEndCellCoordinates(string coordinates)
        {
            cellEndCoordinatesTextMesh.text = coordinates;
        }
    }
}
