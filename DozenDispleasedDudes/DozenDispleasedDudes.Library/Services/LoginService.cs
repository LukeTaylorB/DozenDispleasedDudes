using DozenDispleasedDudes.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DozenDispleasedDudes.Library.Services
{
    public class LoginService
    {
        private static LoginService? instance;
        public static Employee activeEmployee{get; set;}
       
        
        private static object _lock = new object();
        public static LoginService Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new LoginService();//how to make this work with a parameter
                    }
                }

                return instance;
            }
        }
        
        
       
    }
}
