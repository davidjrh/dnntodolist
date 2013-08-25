using System.Data;
using DotNetNuke.Data;

namespace DavidRodriguez.Modules.TodoItems.Components
{

    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// An abstract class for the data access layer 
    /// </summary> 
    /// <remarks> 
    /// </remarks> 
    /// <history> 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public abstract class DataProvider
    {

        #region "Shared/Static Methods"

        /// <summary>
        /// singleton reference to the instantiated object 
        /// </summary>
        private static DataProvider _objProvider;

        /// <summary>
        /// constructor
        /// </summary>
        static DataProvider()
        {
            CreateProvider();
        }

        /// <summary>
        /// dynamically create provider 
        /// </summary>
        private static void CreateProvider()
        {
            _objProvider = (DataProvider)DotNetNuke.Framework.Reflection.CreateObject("data", "SqlDataProvider", "DavidRodriguez.Modules.TodoItems.Components", "DavidRodriguez.Modules.TodoItems", true, false);
        }

        /// <summary>
        /// return the provider 
        /// </summary>
        /// <returns></returns>
        public static DataProvider Instance()
        {
            return _objProvider;
        }

        #endregion

        #region "Abstract methods"

        /// <summary>
        /// Gets the TodoItem list for a module
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public abstract IDataReader GetTodoItems(int moduleId);

        /// <summary>
        /// Gets a TodoItem
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public abstract IDataReader GetTodoItem(int moduleId, int itemId);

        /// <summary>
        /// Adds a TodoItem 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="content"></param>
        /// <param name="userId"></param>
        public abstract void AddTodoItem(int moduleId, string content, int userId);


        /// <summary>
        /// Updates a TodoItem
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="itemId"></param>
        /// <param name="content"></param>
        /// <param name="complete"></param>
        /// <param name="userId"></param>
        public abstract void UpdateTodoItem(int moduleId, int itemId, string content, bool complete, int userId);


        /// <summary>
        /// Deletes a TodoItem
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="itemId"></param>
        public abstract void DeleteTodoItem(int moduleId, int itemId);

        #endregion

    }
}