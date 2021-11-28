﻿using Microsoft.Extensions.Configuration;

namespace DagAir.WebAdminApp.Infrastructure
{
    public class ExternalServices : IExternalServices
    {
        public string FacilitiesApi { get; set; }

        public ExternalServices(IConfiguration configuration)
        {
            FacilitiesApi = configuration["serviceUrls:DagAir.Facilities"];
        }
    }
}