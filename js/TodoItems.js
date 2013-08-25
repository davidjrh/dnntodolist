Type.registerNamespace('djrh.TodoItems');
dnn.extend(djrh.TodoItems,
    {
        TodoItemModel: function(todoItems, model) {
            var that = this;

            this.todoItems = todoItems;            
            this.moduleId = ko.observable(model.ModuleId);
            this.itemId = ko.observable(model.ItemId);
            this.content = ko.observable(model.Content);
            this.complete = ko.observable(model.Complete);
            this.createdByUser = ko.observable(model.CreatedByUser);
            this.createdDate = ko.observable(model.CreatedDate);
            this.createdByUsername = ko.observable(model.CreatedDate);

            this.cssClass = function() {
                return that.complete() ? "complete" : "";
            };

            this.update = function () {
                that.todoItems.loading(true);
                that.todoItems.ajax("Update", {
                    ItemId: that.itemId,
                    Content: that.content,
                    Complete: that.complete
                    },
                    function (data) {
                        that.todoItems.refresh();
                    },
                    function (e) {
                        that.todoItems.loading(false);
                    }, null, "POST"
                );
            };

            this.remove = function() {
                that.todoItems.loading(true);
                that.todoItems.ajax("Remove", {
                    ItemId: that.itemId
                },
                    function (data) {
                        that.todoItems.refresh();
                    },
                    function (e) {
                        that.todoItems.loading(false);
                    }, null, "POST"
                );
            };
        },
        TodoItemsModel: function() {
            var that = this;
            var sf = $.dnnSF(dnn.getVar("moduleId"));
            
            this.items = ko.observableArray();
            this.loading = ko.observable(true);
            
            function setHeaders(xhr) {
                sf.setModuleHeaders(xhr);
            }
           
            function ajax(webapimethod, parameters, success, failure, complete, method) {
                $("#errorlog").text("");
                var url = sf.getServiceRoot('TodoItems') + 'TodoItems/' + webapimethod;
                method = method || "GET";
                $.ajax({
                    url: url,
                    beforeSend: setHeaders,
                    contentType: 'application/json; charset=UTF-8',
                    type: method,
                    data: parameters ? ko.toJSON(parameters) : null,
                    success: success,
                    error: function (e) {
                        var text = e + (e.request ? ' - ' + e.request.status : '');
                        $("#errorlog").text(text);
                        if (failure)
                            failure();
                    },
                    complete: complete
                });
            }

            this.ajax = ajax;

            this.refresh = function () {
                that.loading(true);
                ajax("GetAll", null,
                    function(data) {
                        that.items.removeAll();
                        $.each(data,
                            function(index, todoItem) {
                                that.items.push(new djrh.TodoItems.TodoItemModel(that, todoItem));
                            });                        
                        that.loading(false);
                    },
                    function(e) {
                        that.loading(false);
                    }
                );
            };

            this.addItem = function (evt) {
                var textbox = $("#newItemContent"),
                    itemText = textbox.val();
                if (itemText !== '') {
                    ajax("Add", { Content: itemText },
                        function (data) {
                            that.refresh();
                        },
                        function (e) {
                            that.loading(false);
                        }, null, "POST"
                    );
                }
                textbox.val('').focus();
                evt.preventDefault();
            };

            this.refresh();
        }
    });
