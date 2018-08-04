var TreeView = (function () {
    let selection = {};
    let url = {};
    url.employers = {};
    url.employees = {};

    let setUrls = function (rootUrl, employersEditUrl, employeesEditUrl, employersDeletetUrl) {
        url.root = rootUrl;
        url.employers.edit = employersEditUrl;
        url.employees.edit = employeesEditUrl;
        url.employers.delete = employersDeletetUrl;
    };

    let loadEmployerNode = function(id) {
        $.ajax({
            type: "GET",
            url: url.employers.edit + "/" + id,
            data: function() {
                return { 'id': id };
            },
            success:
                function(data) {
                    $('#DetailsContent').html(data);
                }
        });
    };

    let loadEmployeeNode = function(id) {
        $.ajax({
            type: "GET",
            url: url.employees.edit + "/" + id,
            data: function() {
                return { 'id': id };
            },
            success:
                function(data) {
                    $('#DetailsContent').html(data);
                }
        });
    };

    let selectHandler = function (node, selected, event) {
        selection = selected;
        let nodeName = selection.node.id.split("_");
        let nodeId = nodeName.pop();
        let nodeType = nodeName.pop();

        if (nodeType === "Employer") {
            loadEmployerNode(nodeId);
        }
        if (nodeType === "Employee") {
            loadEmployeeNode(nodeId);
        }
    };

    let deleteEmployer = function (id) {
        var token = $('[name=__RequestVerificationToken]').val();
        $.ajax({
            type: "POST",
            url: url.employers.delete + "/" + id,
            data: {
                'id': id,
                '__RequestVerificationToken': token,
            },
            headers: function () {
            },
            dataType: 'json',
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            success:
                function () {
                    $('#jstree').jstree(true).refresh();
                }
        });
    };

    let deleteEmployee = function (id) {
        var token = $('[name=__RequestVerificationToken]').val();
        $.ajax({
            type: "POST",
            url: url.employees.delete + "/" + id,
            data: {
                'id': id,
                '__RequestVerificationToken': token,
            },
            headers: function () {
            },
            dataType: 'json',
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            success:
                function () {
                    $('#jstree').jstree(true).refresh();
                }
        });
    };

    let deleteHandler = function () {
        let nodeName = selection.node.id.split("_");
        let nodeId = nodeName.pop();
        let nodeType = nodeName.pop();

        if (nodeType === "Employer") {
            deleteEmployer(nodeId);
        }
        if (nodeType === "Employee") {
            deleteEmployee(nodeId);
        }
    };

    var initializeTreeView = function() {
        $(function() {
            $('#TreeContent').jstree({
                'core': {
                    'data': {
                        'url': url.root,
                        'data': function(node) {
                            let nodeId = node.id.split('_').pop();
                            return { 'id': nodeId };
                        }
                    },
                    "themes": {
                        "variant": "large"
                    }
                }
            }).on("select_node.jstree", 
                function(node, selected, event) {
                    selectHandler(node, selected, event);
                });
        });
    };

    return {
        getSelected: function() { return selected; },
        getSelectedNode: function () { return Selection.node; },
        delete: function () { deleteHandler(); },
        initialize: function (rootUrl, employersEditUrl, employeesEditUrl, employersDeleteUrl) {
            setUrls(rootUrl, employersEditUrl, employeesEditUrl, employersDeleteUrl);
            initializeTreeView();
        }
    };
})();