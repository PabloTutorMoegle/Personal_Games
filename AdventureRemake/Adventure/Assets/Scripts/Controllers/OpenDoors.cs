using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    public GameObject door1;
    public GameObject door2;
    public GameObject Doors;

    private int enemiesInside = 0;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemiesInside++;
            SetDoorsActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemiesInside = Mathf.Max(0, enemiesInside - 1);
            if (enemiesInside == 0)
                SetDoorsActive(true);
        }
    }

    private void SetDoorsActive(bool active)
    {
        if (door1 != null) door1.SetActive(active);
        if (door2 != null) door2.SetActive(active);
        if (Doors != null) Doors.SetActive(active);
    }
}
