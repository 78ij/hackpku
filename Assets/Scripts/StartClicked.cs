using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartClicked : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Button button = this.GetComponent<Button>();
        EventTrigger eventTrigger = button.gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry click = new EventTrigger.Entry();
        //   点击事件
        click.eventID = EventTriggerType.PointerClick;

    }
    // Update is called once per frame
    void Update() {

    }

    private void OnClick(BaseEventData pointData)
    {

    }

    private void OnMouseEnter(BaseEventData pointData) {

    }

    private void OnMouseExit(BaseEventData pointData) {

    }
}
