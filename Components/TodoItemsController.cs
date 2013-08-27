using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Security;
using DotNetNuke.Web.Api;

namespace DavidRodriguez.Modules.TodoItems.Components
{

    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// The Controller class for ToDoList 
    /// </summary> 
    /// <remarks> 
    /// </remarks> 
    /// <history> 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public class TodoItemsController : DnnApiController
    {

        #region "Public Methods"

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// gets an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]                
        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, CBO.FillCollection<TodoItem>(DataProvider.Instance().GetTodoItems(ActiveModule.ModuleID)));
            }
            catch (Exception ex)
            {                
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// gets an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="itemId">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]        
        [HttpGet]
        public HttpResponseMessage Get(int itemId)
        {    
            try
            {
                var todoItem = CBO.FillObject<TodoItem>(DataProvider.Instance().GetTodoItem(ActiveModule.ModuleID, itemId));
                return todoItem == null ? Request.CreateResponse(HttpStatusCode.NotFound) : Request.CreateResponse(HttpStatusCode.OK, todoItem);
            }
            catch (Exception ex)
            {                
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// adds an object to the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="todoItem">The TodoItem content</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        [ValidateAntiForgeryToken]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]        
        [HttpPost]
        public HttpResponseMessage Add(TodoItem todoItem)
        {
            try
            {
                if (todoItem == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "TodoItem parameter is null");
                }
                DataProvider.Instance().AddTodoItem(ActiveModule.ModuleID, todoItem.Content, UserInfo.UserID);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);                
            }
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Updates an object to the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="todoItem">The TodoItem object</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        [ValidateAntiForgeryToken]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        [HttpPost]
        public HttpResponseMessage Update(TodoItem todoItem)
        {
            try
            {
                if (todoItem == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "TodoItem parameter is null");
                }

                var dbItem = Get(todoItem.ItemId);
                if (dbItem == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                DataProvider.Instance().UpdateTodoItem(ActiveModule.ModuleID, todoItem.ItemId, todoItem.Content, todoItem.Complete, UserInfo.UserID);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);                
            }
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// deletes an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="todoItem">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        [ValidateAntiForgeryToken]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]        
        [HttpPost]
        public HttpResponseMessage Remove(TodoItem todoItem)
        {
            try
            {
                if (todoItem == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "TodoItem parameter is null");
                }

                var dbItem = Get(todoItem.ItemId);
                if (dbItem == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                DataProvider.Instance().DeleteTodoItem(ActiveModule.ModuleID, todoItem.ItemId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        #endregion

    }
}