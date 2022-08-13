using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;

namespace DemoLibrary
{
    public class SqliteDataAccess
    {
        public static List<PersonModel> LoadPeople()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // v "" jsou sql commandy
                var output = cnn.Query<PersonModel>("select * from Person", new DynamicParameters());
                //překonverutje to na list podle PersonModelu - názvy properties musí přěsně sedět s názvem v databázi jinak se nevyplní
                return output.ToList();
            }
        }
        public static void SavePerson(PersonModel person)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Person (FirstName, LastName) values (@FirstName, @LastName)", person);
            }
        }
        public static void EditPerson(PersonModel selectedPerson)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                //cnn.Execute("update Person set (FirstName, LastName) values (@FirstName, @LastName) where (Id) values (@Id)", selectedPerson);
                //error and trial - wtf how is this workin
                cnn.Execute("UPDATE Person SET(FirstName, LastName) = (@FirstName, @LastName) WHERE Id = @Id", selectedPerson);
            }
        }
        public static void DeletePerson(PersonModel selectedPerson)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("DELETE FROM Person WHERE Id = @Id", selectedPerson);
            }
        }
        public static void DeleteMultiplePerson(List<PersonModel> selectedPersons)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                foreach (PersonModel selPerson in selectedPersons)
                {
                    cnn.Execute("DELETE FROM Person WHERE Id = @Id", selPerson);
                }
            }
        }
        public static List<PersonModel> FindPeople(string firstname, string lastname)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var param = new Dictionary<string, object>()
                {
                    ["firstname"] = $"%{firstname}%",
                    ["lastname"] = $"%{lastname}%"
                };

                var output = cnn.Query<PersonModel>("SELECT * FROM Person WHERE FirstName LIKE @firstname AND LastName LIKE @lastname", param);
                return output.ToList();
            }
        }
        private static string LoadConnectionString(string id ="Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
