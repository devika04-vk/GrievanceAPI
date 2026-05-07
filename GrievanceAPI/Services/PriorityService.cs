namespace GrievanceAPI.Services
{
    public static class PriorityService
    {
        public static int GetPriorityScore(
            string severity,
            string description)
        {
            int severityScore = 0;
            int urgencyScore = 0;
            int impactScore = 0;
            int slaScore = 0;

            description = description.ToLower();

            // SEVERITY SCORE
            if (severity == "Critical")
                severityScore = 40;
            else if (severity == "High")
                severityScore = 30;
            else if (severity == "Medium")
                severityScore = 20;
            else
                severityScore = 10;

            // URGENCY SCORE
            if (description.Contains("urgent") ||
                description.Contains("emergency") ||
                description.Contains("immediately"))
            {
                urgencyScore = 20;
            }
            else if (description.Contains("soon"))
            {
                urgencyScore = 10;
            }
            else
            {
                urgencyScore = 5;
            }

            // IMPACT SCORE
            if (description.Contains("hospital") ||
                description.Contains("school") ||
                description.Contains("public") ||
                description.Contains("market"))
            {
                impactScore = 20;
            }
            else
            {
                impactScore = 10;
            }

            // SLA SCORE
            if (severity == "Critical")
                slaScore = 20;
            else if (severity == "High")
                slaScore = 15;
            else if (severity == "Medium")
                slaScore = 10;
            else
                slaScore = 5;

            return severityScore +
                   urgencyScore +
                   impactScore +
                   slaScore;
        }

        public static string GetPriorityRank(int score)
        {
            if (score >= 80)
                return "P1";

            if (score >= 60)
                return "P2";

            if (score >= 40)
                return "P3";

            return "P4";
        }
    }
}