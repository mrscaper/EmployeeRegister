let DetailView = (function () {
    let container = {};
    let url = {
        employers: {
            new: {}
        },
        employees: {
            new: {}
        }
    };

    let creator = function (containerId, newEmployersUrl, newEmployeesUrl) {
        container = $("#" + containerId);
        url.employers.new = newEmployersUrl;
        url.employees.new = newEmployeesUrl;
    }

    let clear = function () {
        container.html("Awaiting selection..");
    }

    let render = function (html) {
        container.html(html);
    }

    let newEmployee = function () {
        $.ajax({
            type: "GET",
            url: url.employees.new,
            success:
                function (data) {
                    render(data);
                }
        });
    }

    let newEmployer = function () {
        $.ajax({
            type: "GET",
            url: url.employers.new,
            success:
                function (data) {
                    render(data);
                }
        });
    }

    return {
        Initialize: function (containerId, newEmployersUrl, newemployeesUrl) { creator(containerId, newEmployersUrl, newemployeesUrl); },
        Clear: function () { clear(); },
        Render: function (html) { render(html) },
        NewEmployer: function () { newEmployer()},
        NewEmployee: function () { newEmployee()}
    }
})();