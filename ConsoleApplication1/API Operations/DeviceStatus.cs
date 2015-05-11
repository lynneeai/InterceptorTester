﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
	class DeviceStatus : APIOperation
	{
        DeviceStatusJSON json;
		public DeviceStatus(Uri server, DeviceStatusJSON status)
		{
			opHost = server;
			hOp = HTTPOperation.POST;
            json = status;
		}

        //TODO: Code this
		public override string getExpectedResult()
		{
			throw new NotImplementedException();
		}

		public override string ToString()
		{
            return "DeviceStatus";
		}

        public override Uri getUri()
        {	
			return new Uri(opHost, "/api/DeviceStatus/" + opQuery.ToString());
        }

        public override object getJson()
        {
            return json;
        }
    }
}


