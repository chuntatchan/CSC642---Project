using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public float BUFFER = 100f;
    public float ANIMATIONSPEED = 30f;

    public Anim[] animations;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Anim anim in animations) {
            anim.setEndPosition(anim.game_Object.transform.position);
            if (anim.animationToPreform == VNAnimation.flyInFromTop) {
                anim.game_Object.transform.localPosition = new Vector3(0f, (anim.game_Object.transform.position.y + Screen.height + BUFFER));
            } else if (anim.animationToPreform == VNAnimation.flyInFromBottom) {
                anim.game_Object.transform.localPosition = new Vector3(0f, -anim.game_Object.transform.position.y - Screen.height - BUFFER);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Anim anim in animations) {
            if (Vector3.Distance(anim.game_Object.transform.position, anim.getEndPosition()) < 0.1f) {
                anim.game_Object.transform.position = anim.getEndPosition();
            } else {
                anim.game_Object.transform.Translate(-anim.getEndPosition().x / ANIMATIONSPEED, -anim.getEndPosition().y / ANIMATIONSPEED, 0f);
            }
        }
    }
}

[System.Serializable]
public class Anim {
    public GameObject game_Object;
    public VNAnimation animationToPreform;
    private Vector3 endPosition;

    public void setEndPosition(Vector3 position) {
        endPosition = position;
    }

    public Vector3 getEndPosition() {
        return endPosition;
    }

}

public enum VNAnimation {
    none, appear, disappear, fadeIn, fadeOut, flyInFromRight, flyInFromLeft, flyInFromBottom, flyInFromTop, flyOutToLeft, flyOutToRight, flyOutToBottom, flyOutToTop
}
