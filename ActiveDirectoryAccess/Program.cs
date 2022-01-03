using System;
using System.Text;
using System.DirectoryServices;

namespace activeDirectoryLdapExamples
{
    class Program
    {
        private static string TST_LDAP_Path = "LDAP://OU=Test OU,DC = ad,DC = livonia,DC = k12,DC = mi,DC = us";
        private static string PROD_LDAP_Path = "LDAP://OU=People,OU=Livonia Public Schools,DC = ad,DC = livonia,DC = k12,DC = mi,DC = us";
        private static DirectoryEntry LDAP_Connection;

        static void Main(string[] args)
        {
            Console.Clear();
            StringBuilder prompts = new StringBuilder("Please chose a numbered option:" + '\n' + '\n');
            prompts.AppendLine("1: Use Test Environment");
            prompts.AppendLine("2: Use Production Environment");
            prompts.AppendLine("0: Exit");

            Console.Write(prompts.ToString());
            string environmentSelection = Console.ReadLine();
            Console.Clear();

            switch (environmentSelection)
            {
                case "1":
                    LDAP_Connection = createDirectoryEntry(environmentSelection);
                    break;
                case "2":
                    LDAP_Connection = createDirectoryEntry(environmentSelection);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("That is not a valid option.");
                    break;
            }

            while (true)
            {
                Console.Clear();
                prompts = new StringBuilder("Please chose a numbered option:" + '\n' + '\n');
                prompts.AppendLine("1: Show All Info For One User");
                prompts.AppendLine("2: Show Some Info For One User");
                prompts.AppendLine("3: Show Selected Info For All Users");
                prompts.AppendLine("4: Update User Description");
                prompts.AppendLine("5: Add New User");
                prompts.AppendLine("0: Exit");

                Console.Write(prompts.ToString());
                string selection = Console.ReadLine();
                Console.Clear();

                switch (selection)
                {
                    case "1":
                        ShowAllInfoForOneUser();
                        break;
                    case "2":
                        ShowSomeInfoForOneUser();
                        break;
                    case "3":
                        ShowSelectedInfoForAllUsers();
                        break;
                    case "4":
                        UpdateUserDescription();
                        break;
                    case "5":
                        AddNewUser();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("That is not a valid option.");
                        break;
                }

                Console.WriteLine('\n' + "Please press \'Enter\' to continue.");
                Console.ReadLine();
            }
        }

        private static DirectoryEntry createDirectoryEntry(string environment)
        {
            DirectoryEntry ldapConnection = new DirectoryEntry("ad.livonia.k12.mi.us");
            ldapConnection.AuthenticationType = AuthenticationTypes.Secure;

            if (environment == "1")
                ldapConnection.Path = TST_LDAP_Path;
            else if (environment == "2")
                ldapConnection.Path = PROD_LDAP_Path;

            return ldapConnection;
        }

        private static void ShowAllInfoForOneUser()
        {
            Console.Write("Enter user: ");
            String username = Console.ReadLine();

            try
            {
                DirectorySearcher search = new DirectorySearcher(LDAP_Connection);
                search.Filter = "(cn=" + username + ")";

                SearchResult result = search.FindOne();

                if (result != null)
                {
                    ResultPropertyCollection fields = result.Properties;

                    foreach (String ldapField in fields.PropertyNames)
                        foreach (Object myCollection in fields[ldapField])
                            Console.WriteLine(String.Format("{0,-20} : {1}", ldapField, myCollection.ToString()));
                }
                else
                {
                    Console.WriteLine("User not found!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught:\n\n" + e.ToString());
            }
        }

        private static void ShowSomeInfoForOneUser()
        {
            Console.Write("Enter user: ");
            String username = Console.ReadLine();
            Console.Write("Enter info desired (comma-delimited list): ");
            string[] requiredProperties = Console.ReadLine().Split(',');

            try
            {
                DirectorySearcher search = new DirectorySearcher(LDAP_Connection);
                search.Filter = "(cn=" + username + ")";

                foreach (String property in requiredProperties)
                {
                    search.PropertiesToLoad.Add(property);
                }

                SearchResult result = search.FindOne();

                if (result != null)
                {
                    foreach (String property in requiredProperties)
                        foreach (Object myCollection in result.Properties[property])
                            Console.WriteLine(String.Format("{0,-20} : {1}", property, myCollection.ToString()));
                }
                else
                {
                    Console.WriteLine("User not found!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught:\n\n" + e.ToString());
            }
        }

        private static void ShowSelectedInfoForAllUsers()
        {
            Console.Write("Enter property: ");
            String property = Console.ReadLine();

            try
            {
                DirectoryEntry myLdapConnection = LDAP_Connection;

                DirectorySearcher search = new DirectorySearcher(myLdapConnection);
                search.PropertiesToLoad.Add("cn");
                search.PropertiesToLoad.Add(property);

                SearchResultCollection allUsers = search.FindAll();

                foreach (SearchResult result in allUsers)
                {
                    if (result.Properties["cn"].Count > 0 && result.Properties[property].Count > 0)
                    {
                        Console.WriteLine(String.Format("{0,-20} : {1}",
                                          result.Properties["cn"][0].ToString(),
                                          result.Properties[property][0].ToString()));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught:\n\n" + e.ToString());
            }
        }

        private static void UpdateUserDescription()
        {
            Console.Write("Enter user: ");
            String username = Console.ReadLine();

            try
            {
                DirectorySearcher search = new DirectorySearcher(LDAP_Connection);
                search.Filter = "(cn=" + username + ")";
                search.PropertiesToLoad.Add("description");

                SearchResult result = search.FindOne();

                if (result != null)
                {
                    DirectoryEntry entryToUpdate = result.GetDirectoryEntry();

                    var description = entryToUpdate.Properties["description"];
                    if (description.Count == 0)
                        Console.WriteLine("Current description : NULL");
                    else
                        Console.WriteLine("Current description : " + description[0].ToString());

                    Console.Write("\n\nEnter new description : ");

                    String newDescription = Console.ReadLine();

                    if (newDescription.ToUpper() == "NULL" || newDescription.Length == 0)
                    {
                        entryToUpdate.Properties["description"].Value = null;
                        entryToUpdate.CommitChanges();
                        Console.WriteLine("\n\n...description now cleared of value");
                    }
                    else
                    {
                        entryToUpdate.Properties["description"].Value = newDescription;
                        entryToUpdate.CommitChanges();
                        Console.WriteLine("\n\n...new description \"" + newDescription + "\" saved");
                    }
                }
                else
                {
                    Console.WriteLine("User not found!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught:\n\n" + e.ToString());
            }
        }

        private static void AddNewUser()
        {
            Console.Write("Enter user first name: ");
            String first = Console.ReadLine();
            Console.Write("Enter user last name: ");
            String last = Console.ReadLine();
            Console.Write("Enter user password: ");
            String pw = Console.ReadLine();

            String domain = "livoniapublicschools.org";
            object[] password = { pw };
            int limitSizeOfLastNameInUserName = last.Length <= 7 ? last.Length : 7;
            String username = first.Substring(0, 1).ToLower() + last.Substring(0, limitSizeOfLastNameInUserName).ToLower();

            try
            {
                DirectoryEntry user = LDAP_Connection.Children.Add("CN=" + username, "user");

                user.Properties["sn"].Add(last);
                user.Properties["userprincipalname"].Add(username + "@" + domain);
                user.Properties["givenname"].Add(first);
                user.Properties["mail"].Add(username + "@" + domain);
                user.Properties["displayname"].Add(first + " " + last);
                user.Properties["samaccountname"].Add(username);

                user.CommitChanges();
                user.Invoke("SetPassword", password);
                user.CommitChanges();

                Console.WriteLine("Account created!");
            }

            catch (Exception e)
            {
                Console.WriteLine("Exception caught:\n\n" + e.ToString());
            }
        }
    }
}