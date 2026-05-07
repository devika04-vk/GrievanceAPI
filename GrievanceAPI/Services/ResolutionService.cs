namespace GrievanceAPI.Services
{
    public static class ResolutionService
    {
        // INITIAL STATUS
        public static string GetInitialStatus()
        {
            return "Submitted";
        }

        // UPDATE STATUS
        public static string UpdateStatus(string currentStatus)
        {
            if (currentStatus == "Submitted")
            {
                return "In Progress";
            }

            if (currentStatus == "In Progress")
            {
                return "Resolved";
            }

            if (currentStatus == "Resolved")
            {
                return "Closed";
            }

            return currentStatus;
        }

        // REOPEN COMPLAINT
        public static string ReopenComplaint()
        {
            return "Reopened";
        }
    }
}