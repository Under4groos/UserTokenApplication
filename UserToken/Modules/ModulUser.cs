namespace UserToken.Modules
{
    public class ModulUser
    {

        private string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        /// <summary>
        /// 1. User name 
        /// 2. User pasword 
        /// </summary>
        Dictionary<string, (string , string)> Users = new Dictionary<string, (string , string)>();
        public ModulUser(WebApplication webApplication , string prefix = "/")
        {
            var todosApi = webApplication.MapGroup(prefix);
            todosApi.MapGet("/registr", (string username , string userpassword) =>
            {
                string data_ = username + userpassword;

                string newdata_ = Reverse(data_);


                Users.Add(username, (userpassword, newdata_));

                return $"{data_} : {newdata_}";

            });
            todosApi.MapGet("/GetUsers", () =>
            {
                string data_ = "";
                foreach (var item in Users)
                {



                    data_ += $"{item.Key} ";
                }
                return data_;
            });


        }


    }
}
