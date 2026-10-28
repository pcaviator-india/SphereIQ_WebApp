import { prisma } from "@/lib/prisma";
import { calculateTrajectory, Collar, SurveyStation } from "@/lib/survey-math";

export async function recalculateHoleTrajectory(holeId: string) {
  try {
    const hole = await prisma.hole.findUnique({
      where: { id: holeId },
      include: {
        collar: true,
        surveys: { orderBy: { measuredDepth: "asc" } },
      },
    });

    if (!hole || hole.surveys.length === 0) return;

    // Use collar if available, otherwise default collar at 0,0,0
    const collar: Collar = hole.collar
      ? {
          northing: Number(hole.collar.northing),
          easting: Number(hole.collar.easting),
          elevation: Number(hole.collar.elevation),
        }
      : {
          northing: 0,
          easting: 0,
          elevation: 0,
        };

    const stations: SurveyStation[] = hole.surveys.map((s) => ({
      measuredDepth: Number(s.measuredDepth),
      inclination: Number(s.inclination),
      azimuth: Number(s.azimuth),
    }));

    const trajectory = calculateTrajectory(collar, stations);

    await prisma.$transaction(
      trajectory.map((calc, idx) =>
        prisma.survey.update({
          where: { id: hole.surveys[idx].id },
          data: {
            trueVerticalDepth: calc.trueVerticalDepth,
            northing: calc.northing,
            easting: calc.easting,
            verticalSection: calc.verticalSection,
          },
        })
      )
    );
  } catch (err) {
    console.error("Auto recalculate trajectory error:", err);
  }
}
