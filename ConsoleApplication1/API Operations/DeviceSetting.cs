﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class DeviceSetting : APIOperation
    {
        public DeviceSetting(Uri server, string serialNum)
        {
            opHost = server;
            hOp = HTTPOperation.GET;
            opQuery = new HTTPQuery(QueryParameter.i, serialNum);
        }

        //TODO: Implement expected result calculation
        public override string getExpectedResult()
        {
            return "I have not a dang clue what this should be";
        }

        public override string ToString()
        {
            return "DeviceSetting";
        }

        public override Uri getUri()
        {
            return new Uri(opHost, "/api/DeviceSetting/" + opQuery.ToString());
        }

        public override object getJson()
        {
            return null;
        }
    }
}
