using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTry.Static
{
    public static class DoctorsId
    {
        static Dictionary<string, int> doctorsId = new Dictionary<string, int>
        {
            {"a4f3213b-9190-4547-8e8a-933531a996bb", 2},
        };

        public static int getDoctorId(string id)
        {
            int returnValue = 0;
            if (doctorsId.TryGetValue(id, out returnValue))
                return returnValue;
            else return 0;
        }
    }
}