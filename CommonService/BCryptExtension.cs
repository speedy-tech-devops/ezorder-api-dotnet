using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices
{
    public class BCryptExtension
    {
        public static async Task<string> Encrypt(string stringText) 
        {
            return await Task.FromResult(BCrypt.Net.BCrypt.HashPassword(stringText));
        }
    }
}
