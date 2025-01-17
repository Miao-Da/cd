using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dpGame;
public class SceneActionManager : SSActionManager, ISSActionCallback {
    CCMoveToAction boatAction;
    CCSequenceAction characterAction;
    Controller controller;
    Judger judger;
    private void Start()
    {
        controller = Director.getInstance().currentSceneController as Controller;
        controller.actionManager = this;
        judger = controller.judger;
    }

    public void moveBoat(GameObject boat, Vector3 pos, float speed) {
        judger.setForbid(true);
        boatAction = CCMoveToAction.GetSSAction(pos, speed);
        Debug.Log("Ready to run!");
        this.RunAction(boat, boatAction, this);
    }
    public void moveCharacter(GameObject chr, Vector3 pos, float speed) {
        judger.setForbid(true);
        Vector3 start = chr.transform.position;
        Vector3 tmp = pos;
        if (start.y > pos.y) {
            tmp.y = start.y;
        }
        else if (start.y < pos.y) {
            tmp.x = start.x;
        }
        SSAction act1 = CCMoveToAction.GetSSAction(tmp, speed);
        SSAction act2 = CCMoveToAction.GetSSAction(pos, speed);
        characterAction = CCSequenceAction.GetSSAcition(1, 0, new List<SSAction>{act1, act2});
        this.RunAction(chr, characterAction, this);
    }
    public void SSActionEvent(SSAction action) {
        judger.setForbid(false);
    }
}