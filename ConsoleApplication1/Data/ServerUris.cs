using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class ServerUris
    {
        static List<Uri> UriList;

        public static List<Uri> getAllUris()
        {
            buildList();
            return UriList;
        }

        //Get most up-to-date server
        public static Uri getLatest()
        {
            buildList();
            return UriList[0];
        }

        //Add new servers here, new ones at the top
        private static void buildList()
        {
            Uri otherServer = new Uri("http://c1c735b6e41a4325897fb74bf2f8927c.cloudapp.net/");
            Uri testServer = new Uri("http://cozumotesttls.cloudapp.net");
        }
    }
}
