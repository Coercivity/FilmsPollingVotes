namespace LobbyService.Helpers
{
    public static class KinopoiskLinkParser
    {
        private const string LINKBASE = "https://www.kinopoisk.ru/film/";


        public static bool IsLink(string link)
        {
            if (link.Length < LINKBASE.Length)
            {
                return false;
            }

            if (LINKBASE.Equals(link.Substring(0, LINKBASE.Length)))
            {
                return true;
            }
            return false;
        }


        public static string GetFilmIdFromLink(string link)
        {
            return link.Substring(link.LastIndexOf('/', link.Length - 2) + 1);
        }

    }
}
