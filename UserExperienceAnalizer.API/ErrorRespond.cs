using System;
using System.Reflection.Metadata.Ecma335;
using UserExperienceAnalizer.API.Models;

namespace UserExperienceAnalizer.API
{
    public class ErrorRespond
    {
        private CommonRespond<object> _response;

        public ErrorRespond()
        {
            this._response = new CommonRespond<object>();    
        }

        public CommonRespond<object> GetErrorRespond(Exception e)
        {
            _response.Response = new Response();
            _response.Response.Message = e.Message;

            return _response;
        }
    }
}
