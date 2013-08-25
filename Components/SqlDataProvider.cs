using System.Data;
using DotNetNuke.ComponentModel;
using Microsoft.ApplicationBlocks.Data;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework.Providers;

namespace DavidRodriguez.Modules.TodoItems.Components
{

    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// SQL Server implementation of the abstract DataProvider class 
    /// </summary> 
    /// <remarks> 
    /// </remarks> 
    /// <history> 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public class SqlDataProvider : DataProvider
    {

        #region "Private Members"

        private const string ProviderType = "data";
        private const string ModuleQualifier = "TodoItems_";

        private readonly ProviderConfiguration _providerConfiguration = ProviderConfiguration.GetProviderConfiguration(ProviderType);
        private readonly string _connectionString;
        private readonly string _providerPath;
        private readonly string _objectQualifier;
        private readonly string _databaseOwner;

        #endregion

        #region "Constructors"

        /// <summary>
        /// Create an instance of the SqlDataProvider
        /// </summary>
        public SqlDataProvider()
        {

            // Read the configuration specific information for this provider 
            var objProvider = (Provider)_providerConfiguration.Providers[_providerConfiguration.DefaultProvider];

            // Read the attributes for this provider 

            //Get Connection string from web.config 
            _connectionString = Config.GetConnectionString();

            if (_connectionString == "")
            {
                // Use connection string specified in provider 
                _connectionString = objProvider.Attributes["connectionString"];
            }

            _providerPath = objProvider.Attributes["providerPath"];

            _objectQualifier = objProvider.Attributes["objectQualifier"];
            if (_objectQualifier != "" & _objectQualifier.EndsWith("_") == false)
            {
                _objectQualifier += "_";
            }

            _databaseOwner = objProvider.Attributes["databaseOwner"];
            if (_databaseOwner != "" & _databaseOwner.EndsWith(".") == false)
            {
                _databaseOwner += ".";
            }

        }

        #endregion

        #region "Properties"
        internal string ConnectionString
        {
            get { return _connectionString; }
        }

        internal string ProviderPath
        {
            get { return _providerPath; }
        }
        #endregion

        #region "Private Methods"

        private string GetFullyQualifiedName(string name)
        {
            return _databaseOwner + _objectQualifier + ModuleQualifier + name;
        }

        #endregion

        #region "Public Methods"

        public override IDataReader GetTodoItems(int moduleId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("GetTodoItems"), moduleId);
        }

        public override IDataReader GetTodoItem(int moduleId, int itemId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("GetTodoItem"), moduleId, itemId);
        }

        public override void AddTodoItem(int moduleId, string content, int userId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("AddTodoItem"), moduleId, content, userId);
        }

        public override void UpdateTodoItem(int moduleId, int itemId, string content, bool complete, int userId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UpdateTodoItem"), moduleId, itemId, content, complete, userId);
        }

        public override void DeleteTodoItem(int moduleId, int itemId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("DeleteTodoItem"), moduleId, itemId);
        }

        #endregion

    }
}