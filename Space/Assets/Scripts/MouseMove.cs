using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseMove : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    [SerializeField] private PlayerController player;
    private Vector2 start;
    private Vector2 curent;
    private bool isMove = false;
    [SerializeField] private bool isCan;

    void Start()
    {
        isMove = false;
        isCan = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isCan)
        {
            isMove = false;
            GameController.onMouseClickUp?.Invoke();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isCan)
        {
            if (!isMove)
            {
                start = eventData.position;
                isMove = true;
                GameController.onMouseClickDown?.Invoke();
            }

            curent = eventData.position;

            if ((start - eventData.position).magnitude <= 100)
            {
                player.mousePosition = (curent - start) * 0.12f;
            }
            else
            {
                player.mousePosition = (curent - start).normalized * 12;
            }
        }
    }
    private void OnEnable()
    {
        GameController.onConnectPlanet += Connect;
        GameController.onDisConnectPlanet += DisConnect;
    }

    private void OnDisable()
    {
        GameController.onConnectPlanet -= Connect;
        GameController.onDisConnectPlanet -= DisConnect;
    }

    private void Connect(int id)
    {
        isCan = true;
    }

    private void DisConnect()
    {
        isCan = false;
    }
}
