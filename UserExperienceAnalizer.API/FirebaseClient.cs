using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserExperienceAnalizer.API
{
    public class FirebaseClient
    {
        public IFirebaseClient InitFirebaseClient()
        {
            string key = "q7N1bx2KdqKgrF0lkULSnuqsFQlHEc0yjyVKgaOW";
            string url = "https://userexperianceanalyzer-default-rtdb.firebaseio.com/";
            string appName = "UserExAnalyser";

            IFirebaseClient client;

            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = key,
                BasePath = url,
            };

            client = new FireSharp.FirebaseClient(config);

            return client;
        }
    }
}
