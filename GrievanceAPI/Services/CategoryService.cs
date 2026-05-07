namespace GrievanceAPI.Services
{
    public static class CategoryService
    {
        public static string GetCategory(string desc)
        {
            desc = desc.ToLower();

            // ROAD
            if (desc.Contains("road") ||
                desc.Contains("pothole") ||
                desc.Contains("bridge") ||
                desc.Contains("traffic"))
            {
                return "Road";
            }

            // WATER
            if (desc.Contains("water") ||
                desc.Contains("pipe") ||
                desc.Contains("leakage") ||
                desc.Contains("drainage") ||
                desc.Contains("sewage"))
            {
                return "Water";
            }

            // ELECTRICITY
            if (desc.Contains("electricity") ||
                desc.Contains("current") ||
                desc.Contains("transformer") ||
                desc.Contains("streetlight"))
            {
                return "Electricity";
            }

            // SANITATION
            if (desc.Contains("garbage") ||
                desc.Contains("waste") ||
                desc.Contains("cleaning"))
            {
                return "Sanitation";
            }

            // HEALTHCARE
            if (desc.Contains("hospital") ||
                desc.Contains("medical") ||
                desc.Contains("health"))
            {
                return "Healthcare";
            }

            // EMERGENCY
            if (desc.Contains("flood") ||
                desc.Contains("fire") ||
                desc.Contains("accident"))
            {
                return "Emergency";
            }

            // GOVERNMENT SERVICES
            if (desc.Contains("certificate") ||
                desc.Contains("ration") ||
                desc.Contains("office"))
            {
                return "Administration";
            }

            return "General";
        }
    }
}