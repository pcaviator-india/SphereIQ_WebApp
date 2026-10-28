using System;

namespace EzyPlan_Interface_V2;

internal interface IEzyPlanProcedure
{
	event EventHandler Finished;

	void Resize();

	void DriveArrived(string driveLetter);

	void DriveRemoved();

	void Start();
}
