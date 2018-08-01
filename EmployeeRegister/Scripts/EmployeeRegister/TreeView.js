var TreeView = (function () {
    let selection = {}
    let url = {}

    let setUrls = function (rootUrl, employersUrl, employeesUrl) {
        url.root = rootUrl;
        url.employers = employersUrl;
        url.employees = employeesUrl;
    }

    let loadEmployerNode = function (id) {
        $.ajax({
            type: "GET",
            url: url.employers + "/" + id,
            data: function () {
                return { 'id': id };
            },
            success:
                function (data) {
                    $('#DetailsContent').html(data);
                }
        });
    }

    let loadEmployeeNode = function (id) {
        $.ajax({
            type: "GET",
            url: url.employee + "/" + id,
            data: function () {
                return { 'id': id };
            },
            success:
                function (data) {
                    $('#DetailsContent').html(data);
                }
        });
    }

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

    var initializeTreeView = function () {
        $(function () {
            $('#TreeContent').jstree({
                'core': {
                    'data': {
                        'url': url.root,
                        'data': function (node) {
                            let nodeId = node.id.split('_').pop();
                            return { 'id': nodeId }
                        }
                    },
                    "themes": {
                        "variant": "large"
                    }
                }
            }).on('select_node.jstree',
                function (node, selected, event) {
                    selectHandler(node, selected, event)
                });
        });
    }

    return {
        getSelected: function () { return selected; },
        getSelectedNode: function () { return Selection.node; },
        initialize: function (rootUrl, employersUrl, employeesUrl) {
            setUrls(rootUrl, employersUrl, employeesUrl);
            initializeTreeView();
        }
    }
})();