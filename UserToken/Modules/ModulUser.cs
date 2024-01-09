namespace UserToken.Modules
{
    public enum UserAcces
    {
        user,
        admin 
    }

    public class ModulUser
    {

        /// <summary>
        /// 1. User name 
        /// 2. User pasword 
        /// </summary>
        Dictionary<string, (string, string , UserAcces)> Users = new Dictionary<string, (string, string , UserAcces)>();




        private string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        string _create_token(string username, string userpassword)
        {
            return Reverse(username + userpassword);
        }
        
        void _registr_user(string username, string userpassword , UserAcces userAcces)
        {

            if (Users.ContainsKey(username))
            {
                Users[username] = (userpassword, _create_token(username, userpassword), userAcces);
            }
            else
            {
                Users.Add(username, (userpassword, _create_token(username, userpassword), userAcces));
            }

            




        }


        Random random = new Random();

        public ModulUser(WebApplication webApplication , string prefix = "/")
        {
            string _daata_password = "";
            for (int i = 0; i < 10; i++)
            {
                _daata_password = "";
                for (int d = 0; d < 10; d++)
                {
                    _daata_password += random.Next(0, 100);
                }
                _registr_user($"username-{i}", _daata_password , UserAcces.user);
            }
           



            var todosApi = webApplication.MapGroup(prefix);
            todosApi.MapGet("/registr", (string username , string userpassword , int UserAcces) =>
            {

                _registr_user(username, userpassword , (UserAcces)UserAcces);




            });
            todosApi.MapGet("/Login", (string username, string userpassword) =>
            {
                if (Users.ContainsKey(username))
                {
                    if (Users[username].Item1 == userpassword)
                    {
                        return Users[username].Item2;
                    }
                }
                return "Error! Вы ввели не тот пароль ибо ... ";

            });


            /// Хочу получить информацию о человеке имеея токен админа и имя пользователя
            todosApi.MapGet("/GetUser/", (string token , string username) =>
            {
                foreach (var item in Users)
                {
                    
                }
            });




            todosApi.MapGet("/GetUsers", () =>
            {
                string data_ = "";
                foreach (var item in Users)
                {
                    data_ += $"UserName: {item.Key}\n\tUserPass: {item.Value.Item1}\n\tUser Token: {item.Value.Item2}\n\tUser atc: {item.Value.Item3}\n";
                }
                return data_;
            });


        }


    }
}
