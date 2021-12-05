using UnityEngine;

public class MainMenu : MonoBehaviour
 {
    [SerializeField] private BirdsMovement BM;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        BM.Pause();
    }

    public void StartGame()
    {
        gameObject.SetActive(false);
        BM.UnPause();
    }
}
