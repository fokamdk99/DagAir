using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAdminApp1.Views.Facilities
{
    public static class ManageFacilitiesPages
    {
        public static string DownloadOrganizations => "DownloadOrganizations";
        public static string DownloadAffiliates => "DownloadAffiliates";
        public static string DownloadOrganizationsNavClass(ViewContext viewContext) => PageNavClass(viewContext, DownloadOrganizations);
        public static string DownloadAffiliatesNavClass(ViewContext viewContext) => PageNavClass(viewContext, DownloadAffiliates);
        
        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                             ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}