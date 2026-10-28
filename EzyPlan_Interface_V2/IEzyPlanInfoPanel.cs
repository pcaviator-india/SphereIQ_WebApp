using EzyPlanStorage;

namespace EzyPlan_Interface_V2;

internal interface IEzyPlanInfoPanel
{
	void SetDrillHoleFile(ref DrillHoleFile drillHoleFile);

	void UpdateFieldValues();
}
