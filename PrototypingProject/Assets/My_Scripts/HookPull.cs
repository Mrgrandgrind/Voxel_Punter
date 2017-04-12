namespace VRTK.Examples
{
	using UnityEngine;

	public class HookPull : VRTK_DestinationMarker
	{
		public void pull (GameObject Controller, Transform destination)
		{
			var controller = (Controller.GetComponent<VRTK_ControllerEvents>() ? Controller.GetComponent<VRTK_ControllerEvents>() : Controller.GetComponentInParent<VRTK_ControllerEvents>());
			var distance = Vector3.Distance(transform.position, destination.position);
			var controllerIndex = VRTK_DeviceFinder.GetControllerIndex(controller.gameObject);
			OnDestinationMarkerSet(SetDestinationMarkerEvent(distance, destination, new RaycastHit(), destination.position, controllerIndex));
		}
	}
}