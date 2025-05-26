using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Domain
{ 
    public  static class Tsecret
    {  
        public static readonly string ClientIdProd = "e98bf70a-9c3b-4c38-8580-4b7f936f1e6e";
        public static readonly string TenantIdProd = "35d72887-7230-4bed-869f-58dce1433da2";
        public static readonly string ClientSecretProd = "bA~8Q~iMNtwySZJ9jh.41nh-k2mu89GkHZRGLbUA";
        public static readonly string UrlProd = "https://graph.microsoft.com/v1.0/me/mailFolders/inbox/messages?$filter=isRead eq false";

        public static readonly string ClientIdTest = "bf24f8a3-15c1-4a6c-acf7-e2ec22325ea8";
        public static readonly string TenantIdTest = "35d72887-7230-4bed-869f-58dce1433da2";
        public static readonly string ClientSecretTest = "jst8Q~1PaAtq44.X3P9quEEVEoGOfJRMLHOSGaf-";
        public static readonly string UrlTest = "https://graph.microsoft.com/v1.0/me/mailFolders/inbox/messages?$filter=isRead eq false";



    }
}
