using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimationController : MonoBehaviour
{
    public AnimationClip doorLeftAnimation;
    public AnimationClip doorRightAnimation;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(StartAnimations);
    }

    public void StartAnimations()
    {
        GetComponent<Animation>().Play(doorLeftAnimation.name);
        GetComponent<Animation>().Play(doorRightAnimation.name);
    }
}