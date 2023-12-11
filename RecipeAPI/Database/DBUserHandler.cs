using MySqlConnector;
using RecipeAPI.Tools;
using System.Text.Json;
using System.Xml.Serialization;

namespace RecipeAPI.Database
{
    public class DBUser
    {
        public string? Name {  get; set; }

        public string? Password { get; set; }
        
        public string? Server { get; set; }

        public string? Database { get; set; }
        public static DBUser BuildUser()
        {
            Console.WriteLine("Creating new DBUser Entry...");
            DBUser user = new DBUser();
            user.AddName();
            user.AddPassword();
            user.AddServer();
            user.AddDatabase();


            return user;
        }

        public void PrintUserData()
        {
            Console.WriteLine(this.Name);
            Console.WriteLine(this.Password);
            Console.WriteLine(this.Server);
            Console.WriteLine(this.Database);
        }
        public void AddName()
        {
            Console.WriteLine("Please enter User Name:");
            this.Name = Console.ReadLine();
        }
        public void AddPassword()
        {
            Console.WriteLine("Please enter Password:");
            this.Password = PasswordWriter.BuildPassword();
        }
        public void AddServer()
        {
            Console.WriteLine("Please enter Server Name:");
            this.Server = Console.ReadLine();
        }
        public void AddDatabase()
        {
            Console.WriteLine("Please enter Database Name:");
            this.Database = Console.ReadLine();
        }
        public void PersistUser()
        {
                //Persist User in secrets.json
                //string newJsonContent = JsonSerializer.Serialize<DBUser>(user, new JsonSerializerOptions() { WriteIndented = true });
                string newJsonContent = JsonSerializer.Serialize<DBUser>(this, new JsonSerializerOptions() { WriteIndented = true });
                Console.WriteLine(newJsonContent);
                File.WriteAllText("./secrets.json", newJsonContent);
        }
        public static DBUser ReadUser(string secretsContent)
        {
            DBUser? user = JsonSerializer.Deserialize<DBUser>(secretsContent, new JsonSerializerOptions()
            { AllowTrailingCommas = true, PropertyNameCaseInsensitive = true });
            return user;
        }
        public IEnumerable<UserStates> ValidateUser()
        {
            List<UserStates> states = new List<UserStates>();
            if (this == null)
            {
                states.Add(UserStates.NoUser);
            }
            if (this.Name == null)
            {
                states.Add(UserStates.NoName);
            }
            if (this.Password == null)
            {
                states.Add(UserStates.NoPassword);
            }
            if (this.Server == null)
            {
                states.Add(UserStates.NoServer);
            }
            if (this.Database == null)
            {
                states.Add(UserStates.NoDatabase);
            }
            return states;
        }
        public void HandleUserState()
        {
            List<UserStates> states = this.ValidateUser().ToList();
            if (states.Count > 0)
            {
                Console.WriteLine("Errors have been spotted mah man");
                foreach(UserStates state in states)
                {
                    Console.WriteLine($"Current State: {state}");
                    switch(state)
                    {
                        case UserStates.NoName:
                            Console.WriteLine("Missing a User Name!");
                            this.AddName();
                            break;
                        case UserStates.NoPassword:
                            Console.WriteLine("Missing a Password!");
                            this.AddPassword();
                            break;
                        case UserStates.NoServer:
                            Console.WriteLine("Missing a Server Name!");
                            this.AddServer(); 
                            break;
                        case UserStates.NoDatabase:
                            Console.WriteLine("Missing a Database Name!");
                            this.AddDatabase();
                            break;
                    }
                }
                HandleUserState();
            }
            else
                //Persist User
                this.PersistUser();
                Console.WriteLine("User fullfills validation criteria");
        }
        public enum UserStates
        {
            NoUser = 0,
            NoName = 1,
            NoPassword = 2,
            NoServer = 3,
            NoDatabase = 4
        }
    }

    public class DBUserHandler
    {
        public DBUser? dbUser;
        public string dbConnectionString { get; }

        public DBUserHandler()
        {
            this.GetUserData();
            this.dbConnectionString = $"Server={dbUser.Server}; User={dbUser.Name}; Password={dbUser.Password}; Database={dbUser.Database}";
            this.TestConnection();
        }
        public void GetUserData()
        {
            if (!File.Exists("./secrets.json"))
            {
                Console.WriteLine("Creating secrets..");
                File.Create("./secrets.json");

                DBUser user = DBUser.BuildUser();
                user.HandleUserState();
                this.dbUser = user;
            }
            else
            {
                Console.WriteLine("Secrets exists.");
                //string asd = File.ReadAllText();
                Console.WriteLine(File.ReadAllText("./secrets.json"));
                string secretsContent = File.ReadAllText("./secrets.json");
                try
                {
                    DBUser? user = DBUser.ReadUser(secretsContent);
                    user.HandleUserState();
                    user.PrintUserData();
                    this.dbUser = user;
                }
                catch (JsonException e)
                {
                    Console.WriteLine(e.Message);

                    //CreateUserEntry
                    DBUser user = DBUser.BuildUser();
                    user.HandleUserState();
                    //Validate User

                    //Persist User in secrets.json
                    // string newJsonContent = JsonSerializer.Serialize<DBUser>(user, new JsonSerializerOptions() { WriteIndented = true });
                    // Console.WriteLine(newJsonContent);
                    // File.WriteAllText("./secrets.json", newJsonContent);
                    this.dbUser = user;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }
        public async void TestConnection()
        {
            using MySqlConnection connection = new MySqlConnection(dbConnectionString);
                Console.WriteLine("Testing Connection");
            try
            {
                await connection.OpenAsync();
                Console.WriteLine(connection.State);
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("------------------------------");
                Console.WriteLine(e.Message);
                Console.WriteLine("------------------------------");
                Console.WriteLine("The given User is unable to connect.");
                Console.WriteLine("Please check the User Data in the secrets.json");
                //return;
                throw new ArgumentException("Invalid User");
            }
        }
    }
}
