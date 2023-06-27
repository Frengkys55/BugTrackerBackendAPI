﻿namespace BugTrackerBackendAPI.Data.Misc
{
    public class StreamWriter
    {
        /// <summary>
        /// Save stream to disk
        /// </summary>
        /// <param name="source">Source of the stream to be saved</param>
        /// <param name="path">Where on the disk the stream should be saved</param>
        /// /// <param name="overrideIfExist">Override existing file when found</param>
        /// <returns></returns>
        public async Task Write(MemoryStream source, string path, bool overrideIfExist = false)
        {
            // check if the file name already exist
            if (File.Exists(path) && !overrideIfExist)
            {
                throw new Exception("There is a file with the same name found");
            }

            try
            {
                using var stream = new FileStream(path, FileMode.Create);
                source.Position = 0;
                source.CopyTo(stream);
                stream.Flush();
                stream.Close();
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}