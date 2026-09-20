using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Ïîëå äëÿ ñòâîðåííÿ ä³¿ ââîäó
    public InputAction MoveAction;

    // Øâèäê³ñòü ðóõó ïåðñîíàæà
    public float speed = 3.0f;

    void Start()
    {
        // Îáîâ'ÿçêîâî âìèêàºìî ä³þ ââîäó ïðè ñòàðò³ ãðè
        MoveAction.Enable();
    }

    void Update()
    {
        // Ç÷èòóºìî íàïðÿìîê (Vector2: X òà Y) â³ä íàòèñíóòèõ êëàâ³ø
        Vector2 move = MoveAction.ReadValue<Vector2>();
        Debug.Log(move);
        // Îòðèìóºìî ïîòî÷íó ïîçèö³þ
        Vector2 position = transform.position;

        // Äîäàºìî çì³ùåííÿ ç óðàõóâàííÿì øâèäêîñò³ òà Time.deltaTime (ïëàâíèé ðóõ ïðè áóäü-ÿêîìó FPS)
        position.x = position.x + speed * move.x * Time.deltaTime;
        position.y = position.y + speed * move.y * Time.deltaTime;

        // Çàñòîñîâóºìî íîâó ïîçèö³þ
        transform.position = position;
    }
}

