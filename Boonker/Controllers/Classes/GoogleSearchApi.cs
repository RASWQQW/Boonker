using Boonker.Data.Models;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Boonker.Controllers { 
    public class GoogleSearchApi
    {
        public List<FoundObjects> Mousae(string title, string author)
        {
            string apiKey = "AIzaSyDXjXBaFUACHhhMnc2Zx2M_LnyBoX8J2Y0";
            string cx = "015598178761323117960:sbbkk2__0lo";
            string query = $"filetype:pdf {title} {author}";

            var svc = new Google.Apis.Customsearch.v1.CustomsearchService(
                new BaseClientService.Initializer { ApiKey = apiKey });

            var listRequest = svc.Cse.List();

            listRequest.Cx = cx; // text
            listRequest.Q = query; // query which needed find
            var search = listRequest.Execute(); //execute lastly

            List<FoundObjects> objects = new List<FoundObjects>();

            foreach (var result in search.Items)
            {
                objects.Add(new FoundObjects
                {
                    Title = result.Title,
                    Url = result.Link
                });
            }

            return objects;
        }
        
    }
}
