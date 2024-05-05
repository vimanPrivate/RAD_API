using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UserExperienceAnalizer.Data
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
