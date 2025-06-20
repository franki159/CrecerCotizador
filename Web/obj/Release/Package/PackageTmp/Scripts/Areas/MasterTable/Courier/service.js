export class CourierService {
    constructor() {
        this.order = null;
    }

    setOrder(order) {
        this.order = order;
    }

    save(courier) {
        let _this = this;
        alertify.dialog('confirm').set({
            transition: 'zoom',
            title: 'SISPOC',
            labels: { ok: 'Ok', cancel: 'Cancelar' },
            message: '¿Está seguro de continuar con esta operación?',
            onok: function () {
                let dataform = new FormData();
                Object.entries(courier).map(([key, value]) =>
                    dataform.append(`courier[${key}]`, value)
                );
                dataform.append('Files', courier.file);
                $.ajax({
                    type: 'POST',
                    url: '/Courier/Save',
                    data: dataform,
                    cache: false,
                    contentType: false,
                    dataType: 'json',
                    processData: false,
                    success: function (data) {
                        _this.order.returnSave(data);
                    },
                    error: function (data) {
                        alertify.dialog('alert').set({
                            transition: 'zoom',
                            message: data.Result,
                            title: 'SISPOC'
                        }).show();
                        console.log('%cÉrror: Could not establish a connection with the controller.', 'color:red');
                        return false;
                    },
                    complete: function (data) {

                    }
                });
            },
            oncancel: function () {
            }
        }).show();
    }

    saveDestination(destination) {
        let _this = this;
        alertify.dialog('confirm').set({
            transition: 'zoom',
            title: 'SISPOC',
            labels: { ok: 'Ok', cancel: 'Cancelar' },
            message: '¿Está seguro de continuar con esta operación?',
            onok: function () {
                $.ajax({
                    type: 'POST',
                    url: '/Courier/SaveDestination',
                    data: JSON.stringify(destination),
                    cache: false,
                    contentType: 'application/json',
                    dataType: 'json',
                    success: function (result) {
                        _this.order.returnDestinationSave(result);
                    },
                    error: function (data) {
                        alertify.dialog('alert').set({
                            transition: 'zoom',
                            message: data.Result,
                            title: 'SISPOC'
                        }).show();
                        console.log('%cÉrror: Could not establish a connection with the controller.', 'color:red');
                        return false;
                    },
                    complete: function (data) {

                    }
                });
            },
            oncancel: function () {
            }
        }).show();
    }

    getListCouriers(filter) {
        let _this = this;
        $.ajax({
            url: '/Courier/GetListCouriers',
            contentType: 'application/json',
            data: filter,
            success: function (data) {
                _this.order.loadCouriersGrid(data);
            },
            error: function () {
                console.log('%cError: The information could not be obtained.', 'color:red');
            }
        });
    }

    getCourier(idCourier) {
        let _this = this;
        $.ajax({
            url: '/Courier/GetCourier',
            contentType: 'application/json',
            data: {
                id: idCourier
            },
            success: function (data) {
                _this.order.loadCourier(data);
            },
            error: function () {
                console.log('%cError: The information could not be obtained.', 'color:red');
            }
        });
    }

    getDestinationsByCourier(filter) {
        let _this = this;
        $.ajax({
            url: '/Courier/GetListDestinationsByCourier',
            contentType: 'application/json',
            data: filter,
            success: function (data) {
                _this.order.loadDestinationsGrid(data);
            },
            error: function () {
                console.log('%cError: The information could not be obtained.', 'color:red');
            }
        });
    }

    getDestination(destination) {
        let _this = this;
        $.ajax({
            url: '/Courier/GetCourierDestination',
            contentType: 'application/json',
            data: destination,
            success: function (data) {
                _this.order.loadCourierDestination(data);
            },
            error: function () {
                console.log('%cError: The information could not be obtained.', 'color:red');
            }
        });
    }
}