﻿using BugTrackerBackendAPI.Data.DbHelper;
using Microsoft.OpenApi.Writers;
using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class Project
    {

        /// <summary>
        /// Update project from database with the current given information
        /// </summary>
        /// <param name="project">Infomration about the project to be updated</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public void UpdateProject(Project project, string connectionString)
        {
            GenericWrite<Project> genericWrite = new GenericWrite<Project>();
            List<string> ignoreList = new List<string>
            {
                "Id",
                "DateModified",
                "DateCreated",
            };
            if (genericWrite.WriteUsingProcedure(connectionString, "EditProject", project, ignoreList) != 1)
            {
                throw new Exception("Fail to update project");
            } 
        }
    }

}
