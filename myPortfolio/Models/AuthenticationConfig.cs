using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Models
{
    public class AuthenticationConfig
    {

        public User User;

        public AuthenticationConfig(String username, String password)
        {
            User = new User();
        }


    }
}
