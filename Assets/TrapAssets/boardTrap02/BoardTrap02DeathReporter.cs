using UnityEngine;

/// <summary>
/// Forwards Sleepwalker entries from Board Trap 02's death zone to the trap root.
/// </summary>
public class BoardTrap02DeathReporter : MonoBehaviour
{
    private BoardTrap02 parentTrap;

    private void Awake()
    {
        parentTrap = GetComponentInParent<BoardTrap02>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Sleepwalker") || parentTrap == null)
        {
            return;
        }

        SleepwalkerController character = collision.GetComponent<SleepwalkerController>();
        if (character != null)
        {
            parentTrap.OnCharacterEnterZone(character);
        }
    }
}
