using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGS.Domain.Extensions
{
    public static class RandomGenerator
    {
        public static int GenerateCodeAuthenticator()
        {
            Random random = new Random();
            int code = random.Next(100000, 999999);
            return code;
        }
    }
}
