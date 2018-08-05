

let TreeView = (function () {
    let selection = {};
    let url = {
        employers: {
            edit: {},
            delete: {}
        },
        employees: {
            edit: {},
            delete: {}
        }
    };

    let setUrls = function (rootUrl, employersEditUrl, employeesEditUrl, employersDeletetUrl, employeesDeletetUrl) {
        url.root = rootUrl;
        url.employers.edit = employersEditUrl;
        url.employees.edit = employeesEditUrl;
        url.employers.delete = employersDeletetUrl;
        url.employees.delete = employeesDeletetUrl;
    };

    let loadEmployerNode = function (id) {
        $.ajax({
            type:
                "GET",
            url:
                url.employers.edit + "/" + id,
            data:
                function () {
                    return { 'id': id };
                },
            success:
                function (data) {
                    DetailView.Render(data);
                },
            error:
                function (jqXHR, textStatus, errorThrown) {
                    alert("An error occurred... Look at the console (F12 or Ctrl+Shift+I, Console tab) for more information!");
                }

        });
    };

    let loadEmployeeNode = function (id) {
        $.ajax({
            type: "GET",
            url: url.employees.edit + "/" + id,
            data:
                function () {
                    return { "id": id };
                },
            success:
                function (data) {
                    DetailView.Render(data);
                },
            error:
                function (jqXHR, textStatus, errorThrown) {
                    alert("An error occurred... Look at the console (F12 or Ctrl+Shift+I, Console tab) for more information!");
                }
        });
    };

    let selectHandler = function (node, select, event) {
        selection = select;
        let selected = getSelected();

        if (selected.Type === "Employer") {
            loadEmployerNode(selected.Id);
        }
        if (selected.Type === "Employee") {
            loadEmployeeNode(selected.Id);
        }
    };

    let getSelected = function () {
        let name = selection.node.id;
        let splitName = selection.node.id.split("_");
        let id = splitName.pop();
        let type = splitName.pop();
        let node = selection.node;

        let result =
            {
                Name: name,
                Id: id,
                Type: type,
                Node: node
            }
        return result;
    }

    let deleteEmployer = function (id, selection) {
        var token = $('[name=__RequestVerificationToken]').val();
        $.ajax({
            type:
                "POST",
            url:
                url.employers.delete + "/" + id,
            data:
                {
                    'id': id,
                    '__RequestVerificationToken': token,
                },
            dataType:
                'json',
            contentType:
                'application/x-www-form-urlencoded; charset=utf-8',
            success:
                function (response) {
                    if (response.success) {
                        DetailView.Clear();
                        if (response.parent!=null) {
                            TreeView.Refresh(response.parent);
                        } else {
                            TreeView.RefreshAll();
                        }
                    } else {
                        alert(result.detail);
                    }
                },
            error:
                function () {
                    alert("An error occurred... Look at the console (F12 or Ctrl+Shift+I, Console tab) for more information!");
                }

        });
    };

    let deleteEmployee = function (id, selection) {
        var token = $('[name=__RequestVerificationToken]').val();
        $.ajax({
            type:
                "POST",
            url:
                url.employees.delete + "/" + id,
            data:
            {
                'id': id,
                '__RequestVerificationToken': token,
            },
            dataType:
                'json',
            contentType:
                'application/x-www-form-urlencoded; charset=utf-8',
            success:
                function (response) {
                    if (response.success) {
                        DetailView.Clear();
                        TreeView.Refresh(response.parent);
                    } else {
                        alert(result.detail);
                    }
                },
            error:
                function () {
                    alert("An error occurred... Look at the console (F12 or Ctrl+Shift+I, Console tab) for more information!");
                }

        });
    };

    var initializeTreeView = function () {
        $(function () {
            $('#TreeContent').jstree({
                'core': {
                    'data': {
                        'url':
                            url.root,
                        'data':
                            function (node) {
                                let nodeId = node.id.split('_').pop();
                                return { 'id': nodeId };
                            }
                    },
                    "themes":
                        {
                            "variant": "large"
                        }
                }
            }).on("select_node.jstree",
                function (node, selection, event) {
                    selectHandler(node, selection, event);
                });
        });
    };


    let deleteHandler = function () {
        let selected = getSelected();

        if (selected.Type === "Employer") {
            deleteEmployer(selected.Id, selection);
        }
        if (selected.Type === "Employee") {
            deleteEmployee(selected.Id, selection);
        }

    };

    let editSuccessHandler = function(response) {
        if (response.success) {
            if (response.parent!=null) {
                TreeView.Refresh(response.parent);
            } else {
                TreeView.RefreshAll();
            }
            if (response.newParent != null) {
                TreeView.Refresh(response.newParent);
            } else {
                TreeView.RefreshAll();
            }
            
        } else {
            alert(response.detail);
        }
    }

    return {
        Selected: function () { return getSelected(); },
        Delete: function () { deleteHandler(); },
        Initialize: function (rootUrl, employersEditUrl, employeesEditUrl, employersDeleteUrl, employeesDeleteUrl) {
            setUrls(rootUrl, employersEditUrl, employeesEditUrl, employersDeleteUrl, employeesDeleteUrl);
            initializeTreeView();
        },
        Tree: function () { return $('#TreeContent').jstree(true); },
        RefreshSelected: function () { TreeView.Tree().refresh_node(TreeView.Selected().Name)},
        Refresh: function (name) { TreeView.Tree().refresh_node(name) },
        RefreshAll: function () { TreeView.Tree().refresh(); },
        EditSuccess: function (result) { editSuccessHandler(result); }
    };
})();