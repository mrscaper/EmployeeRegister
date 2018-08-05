

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

    let selectHandler = function (node, selectedd, event) {
        selection = selectedd;
        let selected = getSelected();

        if (selected.Type === "Employer") {
            loadEmployerNode(selected.Id);
        }
        if (selected.Type === "Employee") {
            loadEmployeeNode(selected.Id);
        }
    };

    let getSelected = function () {
        let name = selection.node.id.split("_");
        let id = name.pop();
        let type = name.pop();
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
                function () {
                    alert("Ajax success");
                },
            error:
                function () {
                    $('#TreeContent').jstree(true).hide_node(selection.node);
                    DetailView.Clear();
                },

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
                function () {
                    alert("Ajax success");
                },
            error:
                function () {
                    $('#TreeContent').jstree(true).hide_node(selection.node);
                    DetailView.Clear();
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

    return {
        Selected: function () { return getSelected(); },
        Delete: function () { deleteHandler(); },
        Initialize: function (rootUrl, employersEditUrl, employeesEditUrl, employersDeleteUrl) {
            setUrls(rootUrl, employersEditUrl, employeesEditUrl, employersDeleteUrl);
            initializeTreeView();
        }
    };
})();