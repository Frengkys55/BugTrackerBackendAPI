namespace BugTrackerBackendAPI.Models.Misc
{
    /// <summary>
    /// Use this object if you want to do something but the database only return one column (like checking)
    /// </summary>
    public class DbResultModel
    {
        public dynamic? Result { get; set; }
    }
}
