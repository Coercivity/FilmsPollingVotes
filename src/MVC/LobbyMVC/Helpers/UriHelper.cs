using System;
using System.Security.Policy;

namespace LobbyMVC.Helpers
{
    public static class UriHelper
    {


        public static string BuildLink(Guid id, string actionPath)
        {
            #region dev

            var uriBuilder = new UriBuilder()
            {
                Scheme = "https",
                Host = "localhost",
                Port = 63032,
                Path = actionPath + $"/{id}",
                Query = null
            };

            #endregion

            return uriBuilder.Uri.AbsoluteUri;
        }

    }
}
