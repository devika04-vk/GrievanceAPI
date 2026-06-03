namespace GrievanceAPI.Services
{
    public static class DepartmentService
    {
        public static string GetDepartment(string category)
        {
            category = category.ToLower();

            if (category.Contains("road"))
                return "Public Works Department";

            if (category.Contains("water"))
                return "Water Authority";

            if (category.Contains("electricity"))
                return "Electricity Board";

            if (category.Contains("garbage") ||
                category.Contains("sanitation"))
                return "Sanitation Department";

            if (category.Contains("health") || 
                    category.Contains("Ambulance"))
                return "Health Department";

            return "General Administration";
        }
    }
}