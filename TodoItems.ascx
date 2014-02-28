<%@ Control Language="C#" Inherits="DavidRodriguez.Modules.TodoItems.TodoItems" AutoEventWireup="true" CodeBehind="TodoItems.ascx.cs" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:DnnJsInclude runat="server" FilePath="~/Resources/Shared/scripts/knockout.js" Priority="10" />
<dnn:DnnJsInclude runat="server" FilePath="/DesktopModules/TodoItems/js/TodoItems.js" />

<!-- ko with: dnn.modules.todoitems -->
<div id="TodoItemsContainer">
    <input id="newItemContent" type="text" />
    <input id="addItem" type="submit" class="dnnPrimaryAction" value="Add" data-bind="click: addItem, clickBubble: false" placeholder='Enter new task' />
    <!-- ko foreach: items -->        
        <div class="itemRow">
            <input id="chkComplete" type="checkbox" data-bind="change: update, checked: complete, click: update" class="dnnCheckbox" />                
            <input type="text" data-bind="value: content, event: { change: update }, attr: { class: 'content ' + cssClass() }"/>
            <input id="removeItem" type="button" class="dnnSecondaryAction" value="Delete" data-bind="click: remove"/>
        </div>
    <!-- /ko -->
    <div class="footer">
        <div data-bind="visible: !loading()"><span class="itemCount" data-bind="text: items().length">0</span> item(s)</div>
        <div data-bind="visible: loading()">Loading...</div>
    </div>
</div>
<!-- /ko -->
<div id="errorlog"></div>

<script type="text/javascript">
    if (typeof djrh === 'undefined') djrh = {};
    $(function() {
        djrh.todoitems = new djrh.TodoItems.TodoItemsModel();
        ko.applyBindings(djrh.todoitems, $("#TodoItemsContainer")[0]);
        $("#newItemContent").focus();
    });
</script>