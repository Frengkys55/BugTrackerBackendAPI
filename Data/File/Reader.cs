namespace BugTrackerBackendAPI.Data.File
{
    public class Reader
    {
        public MemoryStream Read(string path)
        {
            return new MemoryStream(System.IO.File.ReadAllBytes(path));
        }
    }
}
