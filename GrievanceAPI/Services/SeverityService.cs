namespace GrievanceAPI.Services
{
    public static class SeverityService
    {
        public static string GetSeverity(string desc)
        {
            desc = desc.ToLower();

            // CRITICAL
            if (desc.Contains("fire") ||
                desc.Contains("flood") ||
                desc.Contains("death") ||
                desc.Contains("accident") ||
                desc.Contains("explosion"))
            {
                return "Critical";
            }

            // HIGH
            if (desc.Contains("electricity") ||
                desc.Contains("transformer") ||
                desc.Contains("power failure") ||
                desc.Contains("water leakage") ||
                desc.Contains("sewage overflow"))
            {
                return "High";
            }

            // MEDIUM
            if (desc.Contains("garbage") ||
                desc.Contains("drainage") ||
                desc.Contains("waste") ||
                desc.Contains("cleaning"))
            {
                return "Medium";
            }

            // DEFAULT
            return "Low";
        }
    }
}