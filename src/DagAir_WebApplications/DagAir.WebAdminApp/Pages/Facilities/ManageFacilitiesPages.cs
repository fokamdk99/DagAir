using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DagAir.WebAdminApp.Pages.Facilities
{
    public static class ManageFacilitiesPages
    {
        public static string DownloadOrganizations => "DownloadOrganizations";
        public static string DownloadOrganizationsNavClass(ViewContext viewContext) => PageNavClass(viewContext, DownloadOrganizations);
        
        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                             ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}