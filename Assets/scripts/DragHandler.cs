using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public static GameObject itemBeingDragged;
	Vector3 startPosition;
	Transform startParent;

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		itemBeingDragged = gameObject;
		// Play audio if attached on drag
		if (itemBeingDragged.GetComponent<PinyinButton>()) {
			itemBeingDragged.GetComponent<PinyinButton> ().audioSource.Play ();
		}
		startPosition = transform.position;
		startParent = transform.parent;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;


	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		itemBeingDragged = null;
		GetComponent<CanvasGroup>().blocksRaycasts = true;

		// After drag, unity tries to change transform position values so reset with respect to parent
		transform.localPosition = Vector3.zero;
		if(transform.parent == startParent){
			transform.position = startPosition;
		}
	}

	#endregion
}
