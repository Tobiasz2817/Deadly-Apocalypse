using SceneAnimation;
using UnityEngine;

public class PvpRoundInterface : MonoBehaviour
{
    [SerializeField] private TextCountingDown animateRoundText;
    
    public void ShowRoundUI() {
        animateRoundText.InvokeCounting();
    }
    public TextCountingDown.CountDownEvent GetCountEndInvokers() {
        return animateRoundText.GetInvokers();
    }
}
